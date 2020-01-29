namespace Fabulous.XamarinNative.iOS

open Fabulous.XamarinNative
open Fabulous.StaticView
 
type IosStaticViewModel<'model, 'msg>(m: 'model, dispatch: 'msg -> unit, propMap: ViewBindings<'model, 'msg>, viewController: IXamarinNativeProgramHost, debug: bool)  =
    inherit StaticViewModel<'model, 'msg>(m, dispatch, propMap, viewController, debug)

    override this.bind (viewController: IXamarinNativeProgramHost) (elementName: string) (value: obj) =
        ()
        
    override this.bindCmd (viewController: IXamarinNativeProgramHost) (elementName: string) (dispatch: 'msg -> unit) (msg: 'msg) =
        ()
        
    override this.bindValueChanged (viewController: IXamarinNativeProgramHost) (elementName: string) (dispatch: 'msg -> unit) (setter: Setter<'model,'msg>) =
        ()
