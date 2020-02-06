namespace Fabulous.XamarinNative

open Android.Content
open Android.Runtime
open Android.Widget
open System

[<Register("fabulous.xamarinnative.FabulousEditText")>]
type FabulousEditText(context: Context, attributeSet: Android.Util.IAttributeSet) =
    inherit EditText(context, attributeSet)

    [<DefaultValue>] val mutable previousText: String

    let textDidChange = new Event<_>()

    [<CLIEvent>]
    member this.TextDidChange = textDidChange.Publish

    member this.Text 
        with get () = base.Text 
        and set (value) = 
            let cursorPosition = this.SelectionStart
            let oldTextLength = base.Text.Length
            base.Text <- value
            let newCursorPosition = cursorPosition + value.Length - oldTextLength
            this.SetSelection(newCursorPosition)

    override this.OnAttachedToWindow() =
        base.OnAttachedToWindow()

        this.BeforeTextChanged.Add(fun _ -> this.previousText <- this.Text)
        this.AfterTextChanged.Add(fun _ -> if this.previousText <> this.Text then textDidChange.Trigger(this, EventArgs.Empty))
    

    