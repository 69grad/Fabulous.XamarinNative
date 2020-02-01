namespace MultiPageApp

open Fabulous.XamarinNative

module PeopleFormProgram =
    type Model =
        { PersonFirstName: string
          PersonLastName: string }

    type Msg =
        | SetFirstname of string
        | SetLastname of string
        | Save
        | CmdPersistPerson

    type Program(host: IProgramHost) =

        let init() =
            { PersonFirstName = ""
              PersonLastName = "" }, Cmd.none

        let persistPerson model =
            let person =
                { FirstName = model.PersonFirstName
                  LastName = model.PersonLastName }
            PeopleRepository.addPerson person
            Messenger.publish "Person added"
            None

        let update msg model =
            match msg with
            | SetFirstname v -> { model with PersonFirstName = v }, Cmd.none
            | SetLastname v -> { model with PersonLastName = v }, Cmd.none
            | Save -> model, Cmd.ofMsg CmdPersistPerson
            | CmdPersistPerson -> model, Cmd.ofMsgOption (persistPerson model)

        let view() =
            [ "_firstnameTextField" |> Binding.twoWay (fun m -> m.PersonFirstName) (fun v -> SetFirstname v)
              "_lastnameTextField" |> Binding.twoWay (fun m -> m.PersonLastName) (fun v -> SetLastname v)
              "_saveButton" |> Binding.msg Save ]

        do
            Program.mkProgram init update view host
            |> Program.withConsoleTrace
            |> Program.run
            |> ignore
