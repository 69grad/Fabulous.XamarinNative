namespace Fabulous.XamarinNative

open System
open System.Collections.Generic

module Messenger =
    let private subscribers: List<string -> unit> = List<string -> unit>()

    let subscribe (action: string -> unit) =
        subscribers.Add action
        
    // TODO: Implement an unscrubsribe function in order to prevent memory leaks

    let publish (message: string) =
        subscribers |> Seq.iter (fun subscriber -> subscriber (message))
