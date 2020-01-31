// Copyright 2018 Elmish and Fabulous contributors. See https://github.com/fsprojects/Fabulous/blob/master/LICENSE.md for license.
namespace Fabulous.XamarinNative

open System
open System.Diagnostics

/// We store the current dispatch function for the running Elmish program as a
/// static-global thunk because we want old view elements stored in the `dependsOn` global table
/// to be recyclable on resumption (when a new ProgramRunner gets created).
type internal ProgramDispatch<'msg>() =
    static let mutable dispatchImpl = (fun (_msg: 'msg) -> failwith "do not call dispatch during initialization": unit)

    static let dispatch =
        id (fun msg -> dispatchImpl msg)

    static member DispatchViaThunk = dispatch
    static member SetDispatchThunk v = dispatchImpl <- v

/// Program type captures various aspects of program behavior
type Program<'model, 'msg, 'view> =
    { init: unit -> ('model * Cmd<'msg>)
      update: 'msg -> 'model -> ('model * Cmd<'msg>)
      subscribe: 'model -> Cmd<'msg>
      host: IProgramHost
      view: 'view
      debug: bool
      onError: string * exn -> unit }

/// Starts the Elmish dispatch loop for the page with the given Elmish program
type public ProgramRunner<'model, 'msg>(program: Program<'model, 'msg, unit -> ViewBinding<'model, 'msg> list>) =

    do Debug.WriteLine "run: computing initial model"

    // Get the initial model
    let (initialModel, cmd) = program.init()

    let mutable lastModel = initialModel
    let mutable lastViewData = None
    let dispatch = ProgramDispatch<'msg>.DispatchViaThunk

    do Debug.WriteLine "run: computing static components of view"

    // Extract the static content from the view
    //let (mainViewController: IXamarinNativeProgramHost, bindings) = program.view ()
    let bindings = program.view()
    let mainViewController = program.host

    // Start Elmish dispatch loop
    let rec processMsg msg =
        try
            let (updatedModel, newCommands) = program.update msg lastModel
            lastModel <- updatedModel
            try
                updateView updatedModel
            with ex ->
                program.onError ("Unable to update view:", ex)
            try
                newCommands |> List.iter (fun sub -> sub dispatch)
            with ex ->
                program.onError ("Error executing commands:", ex)
        with ex ->
            program.onError ("Unable to process a message:", ex)

    and updateView updatedModel =
        match lastViewData with
        | None ->
            // Construct the binding context for the view model
            let platformViewFactory = PlatformViewFactory.Instance.create
            let platformView: PlatformView<_, _> =
                platformViewFactory (updatedModel, dispatch, bindings, mainViewController, program.debug)

            platformView.SetBindings bindings mainViewController updatedModel dispatch
            lastViewData <- Some(mainViewController, bindings, platformView)

        | Some(page, bindings, platformView) ->
            platformView.UpdateModel bindings updatedModel
            lastViewData <- Some(page, bindings, platformView)


    do
        // Set up the global dispatch function
        //ProgramDispatch<'msg>.SetDispatchThunk (fun msg -> Device.BeginInvokeOnMainThread(fun () -> processMsg msg))
        ProgramDispatch<'msg>.SetDispatchThunk(fun msg -> processMsg msg)

        Debug.WriteLine "updating the initial view"

        updateView initialModel

        Debug.WriteLine "dispatching initial commands"
        for sub in (program.subscribe initialModel @ cmd) do
            sub dispatch

    member __.InitialMainPage = mainViewController

    member __.CurrentModel = lastModel

    member __.Dispatch = dispatch

    /// Set the current model, e.g. on resume
    member __.SetCurrentModel(model, cmd: Cmd<_>) =
        Debug.WriteLine "updating the view after setting the model"
        lastModel <- model
        updateView model
        for sub in program.subscribe model @ cmd do
            sub dispatch

/// Program module - functions to manipulate program instances
[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Program =
    let internal onError (text: string, ex: exn) =
        Console.WriteLine(sprintf "%s: %A" text ex)

    /// Typical program, new commands are produced by `init` and `update` along with the new state.
    let mkProgram (init: unit -> ('model * Cmd<'msg>)) (update: 'msg -> 'model -> ('model * Cmd<'msg>)) (view: 'view)
        (host: IProgramHost) =
        { init = init
          update = update
          view = view
          host = host
          subscribe = fun _ -> Cmd.none
          debug = false
          onError = onError }

    /// Simple program that produces only new state with `init` and `update`.
    let mkSimple (init: unit -> 'model) (update: 'msg -> 'model -> 'model) (view: 'view)
        (host: IProgramHost) =
        mkProgram (fun arg -> init arg, Cmd.none) (fun msg model -> update msg model, Cmd.none) view host

    /// Typical program, new commands are produced discriminated unions returned by `init` and `update` along with the new state.
    let mkProgramWithCmdMsg (init: unit -> ('model * 'cmdMsg list)) (update: 'msg -> 'model -> ('model * 'cmdMsg list))
        (view: 'view) (mapToCmd: 'cmdMsg -> Cmd<'msg>) =
        let convert =
            fun (model, cmdMsgs) ->
                model,
                (cmdMsgs
                 |> List.map mapToCmd
                 |> Cmd.batch)
        mkProgram (fun arg -> init arg |> convert) (fun msg model -> update msg model |> convert) view

    /// Subscribe to external source of events.
    /// The subscription is called once - with the initial (or resumed) model, but can dispatch new messages at any time.
    let withSubscription (subscribe: 'model -> Cmd<'msg>) (program: Program<'model, 'msg, 'view>) =
        let sub model =
            Cmd.batch
                [ program.subscribe model
                  subscribe model ]
        { program with subscribe = sub }

    /// Trace all the messages as they update the model
    let withTrace trace (program: Program<'model, 'msg, 'view>) =
        { program with
              update =
                  fun msg model ->
                      trace msg model
                      program.update msg model }

    /// Handle dispatch loop exceptions
    let withErrorHandler onError (program: Program<'model, 'msg, 'view>) =
        { program with onError = onError }

    /// Set debugging to true
    let withDebug program =
        { program with debug = true }

    let run (program: Program<'model, 'msg, unit -> ViewBinding<'model, 'msg> list>) =
        let program =
            { init = program.init
              update = program.update
              subscribe = program.subscribe
              onError = program.onError
              debug = program.debug
              view = program.view
              host = program.host }
        ProgramRunner(program)

    /// Trace all the updates to the console
    let withConsoleTrace (program: Program<'model, 'msg, _>) =
        let traceInit() =
            try
                let initModel, cmd = program.init()
                Console.WriteLine(sprintf "Initial model: %0A" initModel)
                initModel, cmd
            with e ->
                Console.WriteLine(sprintf "Error in init function: %0A" e)
                reraise()

        let traceUpdate msg model =
            Console.WriteLine(sprintf "Message: %0A" msg)
            try
                let newModel, cmd = program.update msg model
                Console.WriteLine(sprintf "Updated model: %0A" newModel)
                newModel, cmd
            with e ->
                Console.WriteLine(sprintf "Error in model function: %0A" e)
                reraise()

        let traceView() =
            Console.WriteLine(sprintf "View function")
            try
                let info = program.view()
                Console.WriteLine(sprintf "View result: %0A" info)
                info
            with e ->
                Console.WriteLine(sprintf "Error in view function: %0A" e)
                reraise()

        { program with
              init = traceInit
              update = traceUpdate
              view = traceView }
