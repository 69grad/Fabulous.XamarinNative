namespace MultiPageApp

open Fabulous.XamarinNative

module PeopleFormProgram =
    type Model =
        { FirstName: string
          LastName: string }

    type Msg =
        | FirstNameSet of string
        | LastNameSet of string
        | Save
        | CmdPersistPerson

    type Program(host: IProgramHost) =
        let init() =
            { FirstName = ""
              LastName = "" }, Cmd.none

        let persistPerson model =
            let person: Person =
                { FirstName = model.FirstName
                  LastName = model.LastName }
            PeopleRepository.addPerson person
            Messenger.publish "Person added"
            None

        let update msg (model:Model) =
            match msg with
            | FirstNameSet v -> { model with FirstName = v }, Cmd.none
            | LastNameSet v -> { model with LastName = v }, Cmd.none
            | Save -> model, Cmd.ofMsg CmdPersistPerson
            | CmdPersistPerson -> model, Cmd.ofMsgOption (persistPerson model)

        let view() =
            [ "_firstnameTextField" |> Binding.twoWay (fun m -> m.FirstName) (fun v -> FirstNameSet v)
              "_lastnameTextField" |> Binding.twoWay (fun m -> m.LastName) (fun v -> LastNameSet v)
              "_saveButton" |> Binding.msg Save ]

        do
            Program.mkProgram init update view host
            |> Program.withConsoleTrace
            |> Program.run
            |> ignore
