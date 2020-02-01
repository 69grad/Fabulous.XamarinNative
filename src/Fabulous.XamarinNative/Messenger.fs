namespace Fabulous.XamarinNative

open System.Collections.Generic

/// For the sake of demonstrating communication between different
/// programs through the whole app instance. Not production-ready.
module Messenger =
    let private subscribers: List<string -> unit> = List<string -> unit>()

    let subscribe (action: string -> unit) =
        subscribers.Add action
        
    // TODO: Implement an unsubscribe function in order to prevent memory leaks

    let publish (message: string) =
        subscribers |> Seq.iter (fun subscriber -> subscriber (message))
