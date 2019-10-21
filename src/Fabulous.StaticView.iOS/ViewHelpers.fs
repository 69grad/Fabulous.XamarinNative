namespace FabulousStaticViewTest

open System.Reflection
open UIKit

[<AutoOpen>]
module ViewHelpers =

    let getUiElement controller element =
        let propInfo = controller.GetType().GetProperty(element, BindingFlags.NonPublic ||| BindingFlags.Instance)
        propInfo.GetValue(controller)

    let bindLabelText controller element value =
        let label = getUiElement controller element :?> UILabel
        label.Text <- value.ToString()

    let bindButtonAction controller element dispatch msg =
        let button = getUiElement controller element :?> UIButton
        button.TouchDown.Add(fun args -> dispatch msg)
