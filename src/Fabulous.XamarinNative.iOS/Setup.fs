namespace Fabulous.StaticView

    module FabulousIosSetup =
        let initialize() =
            Fabulous.XamarinNative.FactoryWeasel.StaticViewModelFactory <- IosStaticViewModelFactory()
