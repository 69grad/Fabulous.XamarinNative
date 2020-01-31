namespace MultiPageApp

open Fabulous.XamarinNative

module PeopleListProgram =

    type Model =
        { People: Person [] }

    type Msg = Reset

    type Program(host: IXamarinNativeProgramHost) =
        let initModel() = { People = PeopleRepository.people }

        let init() = initModel()

        let update msg _ =
            match msg with
            | Reset -> init()

        let view() =
            [ "People" |> Binding.oneWay (fun m -> m.People) ]

        let runner =
            Program.mkSimple init update view host
            |> Program.withConsoleTrace
            |> Program.runWithStaticView

        do
            let messageReceived = System.Action<string>(fun message -> runner.Dispatch Reset)
            SimpleMessenger.subscribe messageReceived
