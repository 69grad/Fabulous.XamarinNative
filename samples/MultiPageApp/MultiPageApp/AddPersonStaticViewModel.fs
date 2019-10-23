namespace AddPersonStaticViewModel

open Fabulous.Core
open Fabulous.StaticView
open MultiPageApp
open PeopleRepo
open Common
open UIKit

type Model =
  { NewPerson: Person }

type Msg =
    | SetFirstname of string
    | SetLastname of string
    | Save

type AddPersonStaticViewModel (controller : UIViewController) =

    let initModel () = { NewPerson = {Firstname = ""; Lastname = "" } }

    let init () = initModel ()

    let update msg model =
        match msg with
        | SetFirstname v -> { model with NewPerson = { model.NewPerson with Firstname = v } }
        | SetLastname v -> { model with NewPerson = { model.NewPerson with Lastname = v } }
        | Save ->
            model.NewPerson |> addPerson
            model

    let view () =

        controller, [
            "_firstnameTextField" |> Binding.twoWay (fun m -> m.NewPerson.Firstname) (fun v -> SetFirstname v )
            "_lastnameTextField" |> Binding.twoWay (fun m -> m.NewPerson.Lastname) (fun v -> SetLastname v )
            "_saveButton" |> Binding.msg Save
        ]

    let runner =
        Program.mkSimple init update view
        |> Program.withConsoleTrace
        |> Program.runWithStaticView

    interface IStaticViewController
