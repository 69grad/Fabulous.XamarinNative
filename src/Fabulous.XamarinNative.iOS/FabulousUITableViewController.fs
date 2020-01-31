namespace Fabulous.XamarinNative

open UIKit
open System

type FabulousUITableViewController<'t when 't :> IStaticViewController and 't : null>(handle:IntPtr) =
    inherit UITableViewController(handle)
    let mutable staticViewController: 't = null

    override this.ViewDidLoad() =
        base.ViewDidLoad()

        staticViewController <- Activator.CreateInstance(typeof<'t>, this) :?> 't

    interface IXamarinNativeProgramHost
