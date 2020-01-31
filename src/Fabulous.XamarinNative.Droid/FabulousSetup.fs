namespace Fabulous.XamarinNative

module FabulousSetup =
    type DroidPlatformViewFactory() =
        interface IPlatformViewFactory with
            member this.create (updatedModel, dispatch, bindings, host, debug) =
                DroidPlatformView(updatedModel, dispatch, bindings, host, debug) :> PlatformView<_, _>

    let initialize() =
        PlatformViewFactory.Instance <- DroidPlatformViewFactory()
