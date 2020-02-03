namespace Fabulous.XamarinNative

open Android.OS
open System

type FabulousActivity<'t when 't: null>() =
    inherit Android.App.Activity()
    
    [<DefaultValue>] val mutable program: 't

    override this.OnCreate(savedInstanceState: Bundle) =
        base.OnCreate(savedInstanceState)
        this.program <- Activator.CreateInstance(typeof<'t>, this) :?> 't

    interface IProgramHost