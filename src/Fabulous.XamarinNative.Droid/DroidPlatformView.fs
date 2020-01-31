namespace Fabulous.XamarinNative

type DroidPlatformView<'model, 'msg>(m: 'model, dispatch: 'msg -> unit, propMap: ViewBindings<'model, 'msg>, viewController: IProgramHost, debug: bool)  =
    inherit PlatformView<'model, 'msg>(m, dispatch, propMap, viewController, debug)
    
    override this.bind (viewController: IProgramHost) (elementName: string) (value: obj) =
        failwith "TODO"
        
    override this.bindCmd (viewController: IProgramHost) (elementName: string) (dispatch: 'msg -> unit) (msg: 'msg) =
        failwith "TODO"
        
    override this.bindValueChanged (viewController: IProgramHost) (model: 'model) (elementName: string) (dispatch: 'msg -> unit) (setter: Setter<'model,'msg>) =
        failwith "TODO"
