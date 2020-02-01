namespace MultiPageApp

module PeopleRepository =
    let mutable people =
        [| { FirstName = "Joe"
             LastName = "Average" } |]

    let addPerson person =
        people <- people |> Array.append [| person |]
