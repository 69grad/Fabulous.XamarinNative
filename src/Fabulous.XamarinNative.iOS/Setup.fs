namespace Fabulous.XamarinNative

    module FabulousIosSetup =
        let initialize() =
            Fabulous.XamarinNative.FactoryWeasel.StaticViewModelFactory <- IosStaticViewModelFactory()
