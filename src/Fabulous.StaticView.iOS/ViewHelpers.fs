namespace FabulousStaticViewTest

open System.Reflection
open UIKit

[<AutoOpen>]
module ViewHelpers =

    let private getUiElement controller element =
        let propInfo = controller.GetType().GetProperty(element, BindingFlags.NonPublic ||| BindingFlags.Instance)
        propInfo.GetValue(controller)

    let bind controller element value =
        let element = getUiElement controller element
        match element with
        | :? UILabel as label -> label.Text <- value.ToString()
        | _ -> ()

    let bindCmd controller element dispatch msg =
        let element = getUiElement controller element
        match element with
        | :? UIControl as uiControl -> uiControl.TouchDown.Add(fun args -> dispatch msg)
        | _ -> ()
