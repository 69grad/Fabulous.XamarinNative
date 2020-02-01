namespace MultiPageApp

open Fabulous.XamarinNative

module PeopleFormProgram = 
    type Model =
        { NewPerson: Person }

    type Msg =
        | SetFirstname of string
        | SetLastname of string
        | Save
        | CmdPersistPerson of Person

    type Program(host: IProgramHost) =
        let initModel() =
            { NewPerson =
                  { FirstName = ""
                    LastName = "" } }

        let init() = initModel(), Cmd.none
        
        let persistPerson person =
            PeopleRepository.addPerson person
            Messenger.publish "Person added"
            None

        let update msg model =
            match msg with
            | SetFirstname v -> { model with NewPerson = { model.NewPerson with FirstName = v } }, Cmd.none
            | SetLastname v -> { model with NewPerson = { model.NewPerson with LastName = v } }, Cmd.none
            | Save -> model, Cmd.ofMsg (CmdPersistPerson model.NewPerson)
            | CmdPersistPerson person -> model, Cmd.ofMsgOption (persistPerson person)

        let view() =
            [ "_firstnameTextField" |> Binding.twoWay (fun m -> m.NewPerson.FirstName) (fun v -> SetFirstname v)
              "_lastnameTextField" |> Binding.twoWay (fun m -> m.NewPerson.LastName) (fun v -> SetLastname v)
              "_saveButton" |> Binding.msg Save ]

        do
            Program.mkProgram init update view host
            |> Program.withConsoleTrace
            |> Program.run
            |> ignore
