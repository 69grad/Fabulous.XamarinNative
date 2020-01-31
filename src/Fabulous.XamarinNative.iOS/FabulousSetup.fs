namespace Fabulous.XamarinNative

module FabulousSetup =
    let initialize() =
        FactoryWeasel.StaticViewModelFactory <- IosStaticViewModelFactory()
