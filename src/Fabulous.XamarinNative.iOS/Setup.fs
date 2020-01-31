namespace Fabulous.XamarinNative.iOS

    module FabulousIosSetup =
        let initialize() =
            Fabulous.XamarinNative.FactoryWeasel.StaticViewModelFactory <- IosStaticViewModelFactory()
