namespace AddPersonStaticViewModel

open Fabulous.XamarinNative
open MultiPageApp
open Common

type Model =
  { NewPerson: Person }

type Msg =
    | SetFirstname of string
    | SetLastname of string
    | Save

type AddPersonStaticViewModel (host : IXamarinNativeProgramHost) =

    let initModel () = { NewPerson = {Firstname = ""; Lastname = "" } }

    let init () = initModel ()

    let update msg model =
        match msg with
        | SetFirstname v -> { model with NewPerson = { model.NewPerson with Firstname = v } }
        | SetLastname v -> { model with NewPerson = { model.NewPerson with Lastname = v } }
        | Save ->
            PeopleRepo.addPerson model.NewPerson
            SimpleMessenger.publish "Person added"
            model

    let view () =
        [
            "_firstnameTextField" |> Binding.twoWay (fun m -> m.NewPerson.Firstname) (fun v -> SetFirstname v )
            "_lastnameTextField" |> Binding.twoWay (fun m -> m.NewPerson.Lastname) (fun v -> SetLastname v )
            "_saveButton" |> Binding.msg Save
        ]

    let program = Program.mkSimple init update view host
    let runer = program |> Program.runWithStaticView
    
//    let runner =
//        Program.mkSimple init update view
//        |> Program.withConsoleTrace
//        |> Program.runWithStaticView

    interface IStaticViewController
