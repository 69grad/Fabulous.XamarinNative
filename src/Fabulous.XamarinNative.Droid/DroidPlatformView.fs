namespace Fabulous.XamarinNative

open System.Reflection
open Android.App
open Android.Widget

type DroidPlatformView<'model, 'msg>(m: 'model, dispatch: 'msg -> unit, propMap: ViewBindings<'model, 'msg>, viewController: IProgramHost, debug: bool)  =
    inherit PlatformView<'model, 'msg>(m, dispatch, propMap, viewController, debug)

    let getUiElement (viewController: Activity) (elementName: string) =
        let id = viewController.Resources.GetIdentifier(elementName, "id", viewController.PackageName)
        viewController.FindViewById(id)
    
    override this.bind (viewController: IProgramHost) (elementName: string) (value: obj) =
        let element = getUiElement (viewController :?> Activity) elementName
        match element with
        | :? FabulousEditText as label -> label.Text <- value.ToString()
        | :? TextView as label -> label.Text <- value.ToString()
        | :? SeekBar as slider -> slider.SetProgress(value :?> int , false)
        | null ->
            let propInfo = viewController.GetType().GetProperty(elementName, BindingFlags.Public ||| BindingFlags.Instance)
            if propInfo <> null then propInfo.SetValue(viewController, value)
        | _ -> ()
        
    override this.bindCmd (viewController: IProgramHost) (elementName: string) (dispatch: 'msg -> unit) (msg: 'msg) =
        let element = getUiElement (viewController :?> Activity) elementName
        match element with
        | :? Button as uiControl -> uiControl.Click.Add(fun _ -> dispatch msg)
        | _ -> ()
        ()
        
    override this.bindValueChanged (viewController: IProgramHost) (model: 'model) (elementName: string) (dispatch: 'msg -> unit) (setter: Setter<'model,'msg>) =
        let element = getUiElement (viewController :?> Activity) elementName
        match element with
        | :? FabulousEditText as textField ->
            textField.TextDidChange.Add(fun _ -> dispatch <| setter textField.Text model)
        | :? SeekBar as slider ->
            slider.ProgressChanged.Add(fun eventArgs -> 
                let value = eventArgs.Progress
                dispatch <| setter value model)
        | _ -> ()
