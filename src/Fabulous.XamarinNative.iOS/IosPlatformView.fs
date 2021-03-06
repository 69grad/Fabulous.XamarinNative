namespace Fabulous.XamarinNative

open Fabulous.XamarinNative
open System
open System.Reflection
open UIKit
 
type IosPlatformView<'model, 'msg>(m: 'model, dispatch: 'msg -> unit, propMap: ViewBindings<'model, 'msg>, viewController: IProgramHost, debug: bool)  =
    inherit PlatformView<'model, 'msg>(m, dispatch, propMap, viewController, debug)

    let getUiElement (viewController: UIViewController) (elementName: string) =
        let propInfo = viewController.GetType().GetProperty(elementName, BindingFlags.NonPublic ||| BindingFlags.Instance)
        if propInfo <> null then
            propInfo.GetValue(viewController)
        else
            null
    
    override this.bind (host: IProgramHost) (elementName: string) (value: obj) =
        let element = getUiElement (host :?> UIViewController) elementName
        match element with
        | :? UILabel as label -> label.Text <- value.ToString()
        | :? UITextField as textField -> textField.Text <- value.ToString()
        | :? UISlider as slider -> slider.SetValue(float32 (value :?> int), false) 
        | null ->
            let propInfo = host.GetType().GetProperty(elementName, BindingFlags.Public ||| BindingFlags.Instance)
            propInfo.SetValue(host, value)
        | _ -> ()
        
    override this.bindCmd (host: IProgramHost) (elementName: string) (dispatch: 'msg -> unit) (msg: 'msg) =
        let element = getUiElement (host :?> UIViewController) elementName
        match element with
        | :? UIControl as uiControl -> uiControl.TouchDown.Add(fun _ -> dispatch msg)
        | _ -> ()
        ()
        
    override this.bindValueChanged (host: IProgramHost) (model: 'model) (elementName: string) (dispatch: 'msg -> unit) (setter: Setter<'model,'msg>) =
        let element = getUiElement (host :?> UIViewController) elementName
        match element with
        | :? UITextField as textField ->
            textField.AddTarget(EventHandler (fun sender event ->
                dispatch <| setter textField.Text model)
            , UIControlEvent.EditingChanged)
        | :? UISlider as slider ->
            slider.AddTarget(EventHandler (fun sender event ->
                let value = int(slider.Value + 0.5f)
                dispatch <| setter value model)
            , UIControlEvent.ValueChanged)
        | _ -> ()
