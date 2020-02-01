namespace MultiPageApp

open Fabulous.XamarinNative

module PeopleFormProgram =
    type Model =
        { FirstName: string
          LastName: string }

    type Msg =
        | SetFirstname of string
        | SetLastname of string
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
            | SetFirstname v -> { model with FirstName = v }, Cmd.none
            | SetLastname v -> { model with LastName = v }, Cmd.none
            | Save -> model, Cmd.ofMsg CmdPersistPerson
            | CmdPersistPerson -> model, Cmd.ofMsgOption (persistPerson model)

        let view() =
            [ "_firstnameTextField" |> Binding.twoWay (fun m -> m.FirstName) (fun v -> SetFirstname v)
              "_lastnameTextField" |> Binding.twoWay (fun m -> m.LastName) (fun v -> SetLastname v)
              "_saveButton" |> Binding.msg Save ]

        do
            Program.mkProgram init update view host
            |> Program.withConsoleTrace
            |> Program.run
            |> ignore
