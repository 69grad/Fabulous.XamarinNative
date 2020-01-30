namespace Fabulous.StaticView

open System
open System.Reflection
open Fabulous.XamarinNative
open Fabulous.StaticView
open UIKit
 
type IosStaticViewModel<'model, 'msg>(m: 'model, dispatch: 'msg -> unit, propMap: ViewBindings<'model, 'msg>, viewController: IXamarinNativeProgramHost, debug: bool)  =
    inherit StaticViewModel<'model, 'msg>(m, dispatch, propMap, viewController, debug)

    let getUiElement (viewController: UIViewController) (elementName: string) =
        let propInfo = viewController.GetType().GetProperty(elementName, BindingFlags.NonPublic ||| BindingFlags.Instance)
        if propInfo <> null then
            propInfo.GetValue(viewController)
        else
            null
    
    override this.bind (viewController: IXamarinNativeProgramHost) (elementName: string) (value: obj) =
        let element = getUiElement (viewController :?> UIViewController) elementName
        match element with
        | :? UILabel as label -> label.Text <- value.ToString()
        | :? UITextField as textField -> textField.Text <- value.ToString()
        | null ->
            let propInfo = viewController.GetType().GetProperty(elementName, BindingFlags.Public ||| BindingFlags.Instance)
            propInfo.SetValue(viewController, value)
        | _ -> ()
        
    override this.bindCmd (viewController: IXamarinNativeProgramHost) (elementName: string) (dispatch: 'msg -> unit) (msg: 'msg) =
        let element = getUiElement (viewController :?> UIViewController) elementName
        match element with
        | :? UIControl as uiControl -> uiControl.TouchDown.Add(fun args -> dispatch msg)
        | _ -> ()
        ()
        
    override this.bindValueChanged (viewController: IXamarinNativeProgramHost) (model: 'model) (elementName: string) (dispatch: 'msg -> unit) (setter: Setter<'model,'msg>) =
        let element = getUiElement (viewController :?> UIViewController) elementName
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

type IosStaticViewModelFactory() =
    interface IStaticViewModelFactory with
        member this.create (updatedModel, dispatch, bindings, host, debug) =
            IosStaticViewModel(
                updatedModel,
                dispatch,
                bindings,
                host,
                debug) :> StaticViewModel<_,_>
