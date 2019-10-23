module MultiPageApp.PeopleRepo

open Common

let mutable people = [||]

let addPerson (p: Person) =
    people <- Array.append people [|p|]
