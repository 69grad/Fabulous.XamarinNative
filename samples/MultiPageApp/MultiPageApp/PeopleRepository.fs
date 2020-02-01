namespace MultiPageApp

/// Don't do that at home, kids... the purpose of this is to
/// simulate persistence. In a real-world application you would
/// never design it like that ;)
module PeopleRepository =
    let mutable people =
        [| { FirstName = "Joe"
             LastName = "Average" } |]

    let addPerson person =
        people <- people |> Array.append [| person |]
