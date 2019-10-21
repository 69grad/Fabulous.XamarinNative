namespace StaticViewCounterApp

open Fabulous.Core
open Fabulous.StaticView
open UIKit

type Model =
  { Count : int
    Step : int }

type Msg =
    | Increment
    | Decrement
    | Reset
    | SetStep of int

type Element = {name:string}

type StaticViewController (controller: UIViewController) =

    let mutable Controller: UIViewController = controller

    let initModel () = { Count = 0; Step = 3 }

    let init () = initModel ()

    let update msg model =
        match msg with
        | Increment -> { model with Count = model.Count + model.Step }
        | Decrement -> { model with Count = model.Count - model.Step }
        | Reset -> init ()
        | SetStep n -> { model with Step = n }

    let view () =

        Controller, [
            "_incrementButton" |> Binding.msg Increment
            "_decrementButton" |> Binding.msg Decrement
            "_resetButton" |> Binding.msg Reset
            "_valueLabel" |> Binding.oneWay (fun m -> m.Count)
        ]

    let runner =
        Program.mkSimple init update view
        |> Program.withConsoleTrace
        |> Program.runWithStaticView
