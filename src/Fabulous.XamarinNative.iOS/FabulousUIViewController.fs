namespace Fabulous.XamarinNative

open UIKit
open System

type FabulousUIViewController<'t when 't: null>(handle: IntPtr) =
    inherit UIViewController(handle)
    
    [<DefaultValue>] val mutable program: 't

    override this.ViewDidLoad() =
        base.ViewDidLoad()
        this.program <- Activator.CreateInstance(typeof<'t>, this) :?> 't

    interface IProgramHost
