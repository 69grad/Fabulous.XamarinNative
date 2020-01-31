namespace MultiPageApp

module PeopleRepository =
    let mutable people =
        [| { Firstname = "Joe"
             Lastname = "Average" } |]

    let addPerson (p: Person) =
        people <- Array.append people [| p |]
