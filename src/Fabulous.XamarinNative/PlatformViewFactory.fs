namespace Fabulous.XamarinNative

type IPlatformViewFactory =
    abstract create:
        'model * ('msg -> unit) * ViewBindings<'model, 'msg> * IProgramHost * bool -> PlatformView<'model, 'msg>

type public PlatformViewFactory =
    [<DefaultValue>]
    static val mutable private instance : IPlatformViewFactory

    static member Instance
        with set (value) = PlatformViewFactory.instance <- value
        and get() = PlatformViewFactory.instance
