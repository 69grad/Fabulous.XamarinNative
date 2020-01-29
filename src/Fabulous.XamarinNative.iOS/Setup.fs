namespace Fabulous.StaticView

    module FabulousIosSetup =
    
        let initialize() =
            StaticView.StaticViewProgramRunner<_,_>.StaticViewModelFactory <-
                (fun (updatedModel, dispatch, bindings, host, debug) ->
                    IosStaticViewModel(
                        updatedModel,
                        dispatch,
                        bindings,
                        host,
                        debug) :> StaticViewModel<_,_>)
