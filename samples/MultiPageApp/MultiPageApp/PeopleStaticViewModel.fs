namespace PeopleStaticViewModel

open Fabulous.Core
open Fabulous.StaticView
open MultiPageApp
open MultiPageApp.Common
open UIKit

type Model =
  { People : Person[] }

type Msg =
    | Reset

type PeopleStaticViewModel (controller : UIViewController) =

    let initModel () = {
        People = PeopleRepo.people;
    }

    let init () = initModel ()

    let update msg model =
        match msg with
        | Reset -> init ()

    let view () =

        controller, [
            "People" |> Binding.oneWay (fun m -> m.People)
        ]

    let runner =
        Program.mkSimple init update view
        |> Program.withConsoleTrace
        |> Program.runWithStaticView


    do
        let messageReceived = System.Action<string>(fun message ->
            runner.Dispatch Reset
        )

        SimpleMessenger.subscribe messageReceived

    interface IStaticViewController
