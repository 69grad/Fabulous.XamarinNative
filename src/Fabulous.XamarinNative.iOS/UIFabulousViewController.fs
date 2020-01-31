namespace Fabulous.XamarinNative

open UIKit
open System

type UIFabulousViewController<'t when 't :> IStaticViewController and 't : null>(handle:IntPtr) =
    inherit UIViewController(handle)
    
    let mutable staticViewController: 't = null

    override this.ViewDidLoad() =
        base.ViewDidLoad()

        staticViewController <- Activator.CreateInstance(typeof<'t>, this) :?> 't

    interface IXamarinNativeProgramHost
