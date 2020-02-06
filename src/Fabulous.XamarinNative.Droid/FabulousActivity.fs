namespace Fabulous.XamarinNative

open Android.OS
open Android.Support.V7.App
open System

type FabulousActivity<'t when 't: null>() =
    inherit AppCompatActivity()
    
    [<DefaultValue>] val mutable program: 't

    override this.OnCreate(savedInstanceState: Bundle) =
        base.OnCreate(savedInstanceState)
        this.program <- Activator.CreateInstance(typeof<'t>, this) :?> 't

    interface IProgramHost