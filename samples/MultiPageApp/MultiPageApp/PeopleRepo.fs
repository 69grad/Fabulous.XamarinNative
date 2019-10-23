module MultiPageApp.PeopleRepo

open Common

let mutable people = [| {Firstname = "Max"; Lastname = "Mustermann"}  |]

let addPerson (p: Person) =
    people <- Array.append people [|p|]
