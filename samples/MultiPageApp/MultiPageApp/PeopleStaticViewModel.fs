namespace PeopleStaticViewModel

open Fabulous.XamarinNative
open MultiPageApp
open MultiPageApp.Common

type Model =
  { People : Person[] }

type Msg =
    | Reset

type PeopleStaticViewModel (host : IXamarinNativeProgramHost) =

    let initModel () = {
        People = PeopleRepo.people;
    }

    let init () = initModel ()

    let update msg model =
        match msg with
        | Reset -> init ()

    let view () =

        [
            "People" |> Binding.oneWay (fun m -> m.People)
        ]

    let runner =
        Program.mkSimple init update view host
        |> Program.withConsoleTrace
        |> Program.runWithStaticView


    do
        let messageReceived = System.Action<string>(fun message ->
            runner.Dispatch Reset
        )

        SimpleMessenger.subscribe messageReceived

    interface IStaticViewController
