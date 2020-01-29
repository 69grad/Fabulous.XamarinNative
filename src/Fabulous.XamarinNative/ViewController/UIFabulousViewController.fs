namespace Fabulous.StaticView

open UIKit
open System

type IStaticViewController =
    interface end

type UIFabulousViewController<'t when 't :> IStaticViewController and 't : null>(handle:IntPtr) =
    inherit UIViewController(handle)
    let mutable staticViewController: 't = null

    override this.ViewDidLoad() =
        base.ViewDidLoad()

        staticViewController <- Activator.CreateInstance(typeof<'t>, this) :?> 't

