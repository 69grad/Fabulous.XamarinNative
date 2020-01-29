// Copyright 2018 Fabulous contributors. See LICENSE.md for license.
namespace Fabulous.StaticView

open System;

module SimpleMessenger =

    let mutable private recipients: Action<string>[] = [||]

    let subscribe(action: Action<string>) =
        recipients <- Array.append recipients [|action|]

    let publish(message: string) =
        for r in recipients do
            r.Invoke(message)
