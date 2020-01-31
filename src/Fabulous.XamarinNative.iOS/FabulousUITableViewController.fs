namespace Fabulous.XamarinNative

open UIKit
open System

type FabulousUITableViewController<'t when 't: null>(handle: IntPtr) =
    inherit UITableViewController(handle)
    
    [<DefaultValue>] val mutable program: 't

    override this.ViewDidLoad() =
        base.ViewDidLoad()
        this.program <- Activator.CreateInstance(typeof<'t>, this) :?> 't

    interface IProgramHost
