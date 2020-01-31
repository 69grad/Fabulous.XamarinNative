namespace MultiPageApp

module PeopleRepo =
    let mutable people =
        [| { Firstname = "Max"
             Lastname = "Mustermann" } |]

    let addPerson (p: Person) =
        people <- Array.append people [| p |]
