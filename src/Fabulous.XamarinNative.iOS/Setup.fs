namespace Fabulous.StaticView

    module FabulousIosSetup =
        let initialize() =
            Fabulous.StaticView.FactoryWeasel.StaticViewModelFactory <- IosStaticViewModelFactory()
