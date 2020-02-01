namespace MultiPageApp

open Fabulous.XamarinNative

module PeopleListProgram =

    type Model =
        { People: Person [] }

    type Msg =
        | PeopleLoaded of Person []
        | CmdLoadPeople

    type Program(host: IProgramHost) =
        let loadPeople() =
            PeopleLoaded(PeopleRepository.people)

        let init() = { People = [||] }, Cmd.ofMsg CmdLoadPeople

        let update msg model =
            match msg with
            | PeopleLoaded people -> { model with People = people }, Cmd.none
            | CmdLoadPeople -> model, Cmd.ofMsg (loadPeople())

        let view() =
            [ "People" |> Binding.oneWay (fun m -> m.People) ]

        let runner =
            Program.mkProgram init update view host
            |> Program.withConsoleTrace
            |> Program.run

        do Messenger.subscribe (fun _ -> runner.Dispatch CmdLoadPeople)
