namespace MultiPageApp

module PeopleRepository =
    let mutable people =
        [| { FirstName = "Joe"
             LastName = "Average" } |]

    let addPerson (p: Person) =
        people <- Array.append people [| p |]
