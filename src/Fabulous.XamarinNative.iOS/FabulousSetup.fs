namespace Fabulous.XamarinNative

module FabulousSetup =
    type IosPlatformViewFactory() =
        interface IPlatformViewFactory with
            member this.create (updatedModel, dispatch, bindings, host, debug) =
                IosPlatformView(updatedModel, dispatch, bindings, host, debug) :> PlatformView<_, _>

    let initialize() =
        PlatformViewFactory.Instance <- IosPlatformViewFactory()
