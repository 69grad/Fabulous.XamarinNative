﻿namespace CounterApp

open Fabulous.XamarinNative

module CounterProgram =
    type Model =
        { Count: int
          Step: int
          Name: string }

    type Msg =
        | Increment
        | Decrement
        | Reset
        | SetStep of int
        | SetName of string

    type Element =
        { name: string }

    type Program(host: IProgramHost) =
        let initModel() =
            { Count = 0
              Step = 3
              Name = "FSharp" }

        let init() = initModel(), Cmd.none

        let update msg model =
            match msg with
            | Increment -> { model with Count = model.Count + model.Step }, Cmd.none
            | Decrement -> { model with Count = model.Count - model.Step }, Cmd.none
            | Reset -> init()
            | SetStep n -> { model with Step = n }, Cmd.none
            | SetName n -> { model with Name = n }, Cmd.none
            
        let view() =
            [ "_incrementButton" |> Binding.msg Increment
              "_decrementButton" |> Binding.msg Decrement
              "_resetButton" |> Binding.msg Reset
              "_valueLabel" |> Binding.oneWay (fun m -> "Value: " + m.Count.ToString())
              "_stepSizeValueLabel" |> Binding.oneWay (fun m -> m.Step)
              "_stepSizeSlider" |> Binding.twoWay (fun m -> m.Step) (fun v -> SetStep(int (v)))
              "_twoWayFirstTextField" |> Binding.twoWay (fun m -> m.Name) (fun v -> SetName v)
              "_twoWaySecondTextField" |> Binding.twoWay (fun m -> m.Name) (fun v -> SetName v) ]

        do
            Program.mkProgram init update view host
            |> Program.withConsoleTrace
            |> Program.run
            |> ignore
