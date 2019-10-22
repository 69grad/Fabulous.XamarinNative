// Copyright 2018 Fabulous contributors. See LICENSE.md for license.
namespace Fabulous.StaticView

open System
open System.Collections.Generic
open System.ComponentModel
open System.Diagnostics

/// The internal representation of a binding in the ViewModel for static Xaml

type Command = { execute: Action<obj>; canExecute: Func<obj, bool> }

type internal PropertyBinding<'model, 'msg> =
    | Get of Getter<'model>
    | Set of Setter<'model, 'msg>
    | GetSet of Getter<'model> * Setter<'model, 'msg>
    | GetSetValidate of Getter<'model> * ValidSetter<'model, 'msg>
    //| Cmd of Xamarin.Forms.Command
    | Cmd of Command
    | SubModel of ('model -> obj) * (obj -> 'msg) * StaticViewModel<obj, obj>
    | Map of Getter<'model> * (obj -> obj)

and StaticViewModel<'model, 'msg>(m: 'model, dispatch: 'msg -> unit, propMap: ViewBindings<'model, 'msg>, debug: bool) as self =
    inherit System.Dynamic.DynamicObject()

    let props = new Dictionary<string, PropertyBinding<'model, 'msg>>()

    // Store all errors
    let errors = new Dictionary<string, string list>()
    let errorsChanged = new DelegateEvent<System.EventHandler<DataErrorsChangedEventArgs>>()

    // Current model
    let mutable model : 'model = m

    // For INotifyPropertyChanged
    let propertyChanged = Event<PropertyChangedEventHandler, PropertyChangedEventArgs> ()
    let notifyPropertyChanged name = 
        if debug then Trace.WriteLine (sprintf "notifyPropertyChanged %s" name)
        let key = "Item[" + name + "]"
        propertyChanged.Trigger(self, PropertyChangedEventArgs key)

    /// Convert a list of property changes
    let notify (p : string list) =
        p |> List.iter notifyPropertyChanged
        let raiseCanExecuteChanged accessor =
            match accessor with
            //| Cmd c -> c.ChangeCanExecute ()
            | _ -> ()
        props |> List.ofSeq |> List.iter (fun kvp -> raiseCanExecuteChanged kvp.Value)    

    /// Convert a command to a XF command
    let toCommand name (exec, canExec) =
        let execute = 
            Action<obj> (fun cmdParameter -> 
                if debug then Trace.WriteLine (sprintf "view: execute cmd %s" name)
                let msg = 
                   try exec cmdParameter model 
                   with exn -> 
                       if debug then Trace.WriteLine (sprintf "view: execute cmd %s raised exception:\n%s" name (exn.ToString()))
                       reraise()
                dispatch msg)

        let canExecute = 
            Func<obj, bool>(fun cmdParameter -> 
                if debug then Trace.WriteLine (sprintf "view: checking if cmd %s can execute" name)
                canExec cmdParameter model)
        //Xamarin.Forms.Command (execute, canExecute)
        { execute = execute; canExecute = canExecute }

    /// Convert sub-models on receipt of initial bindings
    let convert (name, binding) =
        match binding with
        | Bind getter -> name, Get getter
        | BindOneWayToSource setter -> name, Set setter
        | BindTwoWay (getter, setter) -> name, GetSet (getter, setter)
        | BindTwoWayValidation (getter, setter) -> name, GetSetValidate (getter, setter)
        | BindCmd (exec, canExec) -> name, Cmd (toCommand name (exec, canExec))
        | BindSubModel (ViewSubModel (_, _subName, getter, toMsg, propMap)) -> name, SubModel (getter, toMsg, StaticViewModel<obj, obj>(getter model, toMsg >> dispatch, propMap, debug))
        | BindMap (getter, mapper) -> name, Map (getter, mapper)

    do propMap |> List.map convert |> List.iter props.Add

    // Notifies the view of property changes 
    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member __.PropertyChanged = propertyChanged.Publish
        
    // Notifies the view of validation errors
    interface INotifyDataErrorInfo with
        [<CLIEvent>]
        member __.ErrorsChanged = errorsChanged.Publish
        member __.HasErrors = errors.Count > 0
        member __.GetErrors propName = 
            if debug then Trace.WriteLine (sprintf "Getting errors for %s" propName)
            let results = 
                match errors.TryGetValue propName with
                | true, errs -> errs
                | false, _ -> []
            results :> System.Collections.IEnumerable

    /// Used internally to update the model. Only properties that have changed are updated.
    member __.UpdateModel (other: 'model) : unit =
        if Object.ReferenceEquals (model, other) then 
            if debug then Trace.WriteLine (sprintf "...Skipping update because model is reference-identical")
        //if debug then Trace.WriteLine (sprintf "UpdateModel %+A" (props.Keys |> Seq.toArray))
        let propDiff name prop =
            match prop with
            | Get getter 
            | GetSet (getter, _) 
            | GetSetValidate (getter, _) 
            | Map (getter, _) ->
                let v = getter model
                let otherv = getter other
                if Object.ReferenceEquals (v, otherv) then None 
                elif v <> otherv then Some name 
                else None
            | SubModel (getter, _toMsg, subViewModel) ->
                let otherSubModel = getter other
                subViewModel.UpdateModel otherSubModel
                None
            | _ -> None

        let diffs = 
            props
            |> Seq.choose (fun kvp -> propDiff kvp.Key kvp.Value)
            |> Seq.toList
        
        model <- other
        notify diffs

