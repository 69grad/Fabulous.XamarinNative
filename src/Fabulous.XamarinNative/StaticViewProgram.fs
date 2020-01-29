﻿// Copyright 2018 Fabulous contributors. See LICENSE.md for license.
namespace Fabulous.StaticView

open Fabulous.Core
open Fabulous.XamarinNative
open System
open System.Diagnostics

[<RequireQualifiedAccess>]
module StaticView =
    
    /// Starts the Elmish dispatch loop for the page with the given Elmish program
    type public StaticViewProgramRunner<'model, 'msg>(program: Program<'model, 'msg, unit -> IXamarinNativeProgramHost * ViewBinding<'model,'msg> list>) as self = 

        do Debug.WriteLine "run: computing initial model"

        // Get the initial model
        let (initialModel,cmd) = program.init ()

        let mutable lastModel = initialModel
        let mutable lastViewData = None
        let dispatch = ProgramDispatch<'msg>.DispatchViaThunk

        do Debug.WriteLine "run: computing static components of view"

        // Extract the static content from the view
        let (mainViewController: IXamarinNativeProgramHost, bindings) = program.view ()

        // Start Elmish dispatch loop  
        let rec processMsg msg = 
            try
                let (updatedModel,newCommands) = program.update msg lastModel
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
                let viewModel : StaticViewModel<'model, 'msg> =
                    StaticViewProgramRunner<'model, 'msg>
                        .StaticViewModelFactory(
                            updatedModel,
                            dispatch,
                            bindings,
                            mainViewController,
                            program.debug)
                        
                viewModel.SetBindings bindings mainViewController updatedModel dispatch
                lastViewData <- Some (mainViewController, bindings, viewModel)

            | Some (page, bindings, viewModel)  ->
                viewModel.UpdateModel bindings updatedModel
                lastViewData <- Some (page, bindings, viewModel)
                      
        do 
           // Set up the global dispatch function
           //ProgramDispatch<'msg>.SetDispatchThunk (fun msg -> Device.BeginInvokeOnMainThread(fun () -> processMsg msg))
           ProgramDispatch<'msg>.SetDispatchThunk (fun msg -> processMsg msg)

           Debug.WriteLine "updating the initial view"

           updateView initialModel 

           Debug.WriteLine "dispatching initial commands"
           for sub in (program.subscribe initialModel @ cmd) do
                sub dispatch
                
        static let mutable staticViewModelFactory :
            'model * ('msg -> unit) * ViewBindings<'model, 'msg> * IXamarinNativeProgramHost * bool -> StaticViewModel<'model, 'msg> =
                (fun _ -> failwith "Implement me first!")
            
        static member StaticViewModelFactory
            with private get() = staticViewModelFactory
            and set(value) = staticViewModelFactory <- value

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
[<RequireQualifiedAccess; CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Program =
    /// Add navigation to an application, used only for Half-Elmish Static View.
    let withNavigation (program: Program<_,_,_>) = 
        { init = program.init
          update = program.update
          subscribe = program.subscribe
          onError = program.onError
          debug = program.debug
          view = (fun () -> 
              let page, contents, navMap = program.view ()
              Debug.WriteLine "setting global navigation map"
              // TODO: modify the Elmish framework we use to remove this global state and pass it into all commands??
              //Nav.globalNavMap <- (navMap |> List.map (fun (tg, page) -> ((tg :> System.IComparable), page)) |> Map.ofList)
              page, contents  )}

    let runWithStaticView (program: Program<'model, 'msg, unit -> IXamarinNativeProgramHost * ViewBinding<'model,'msg> list>) = 
        let program = 
            { init = program.init
              update = program.update
              subscribe = program.subscribe
              onError = program.onError
              debug = program.debug
              view = program.view }
        StaticView.StaticViewProgramRunner(program)

    /// Trace all the updates to the console
    let withConsoleTrace (program: Program<'model, 'msg, _>) =
        let traceInit () =
            try 
                let initModel,cmd = program.init ()
                Console.WriteLine (sprintf "Initial model: %0A" initModel)
                initModel,cmd
            with e -> 
                Console.WriteLine (sprintf "Error in init function: %0A" e)
                reraise ()

        let traceUpdate msg model =
            Console.WriteLine (sprintf "Message: %0A" msg)
            try 
                let newModel,cmd = program.update msg model
                Console.WriteLine (sprintf "Updated model: %0A" newModel)
                newModel,cmd
            with e -> 
                Console.WriteLine (sprintf "Error in model function: %0A" e)
                reraise ()

        let traceView () =
            Console.WriteLine (sprintf "View function")
            try 
                let info = program.view ()
                Console.WriteLine (sprintf "View result: %0A" info)
                info
            with e -> 
                Console.WriteLine (sprintf "Error in view function: %0A" e)
                reraise ()
                
        { program with
            init = traceInit 
            update = traceUpdate
            view = traceView }


