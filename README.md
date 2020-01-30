# Fabulous.XamarinNative

## What?

This is a **very early proof of concept** intending to explore possible use cases for [Fabulous](https://github.com/fsprojects/Fabulous) and especially its implementation of the Elm Architecture for native Xamarin apps.

It is based on the "half-elmish approach" of [Fabulous.StaticView](https://github.com/fsprojects/Fabulous/tree/master/Fabulous.StaticView), which was built on top of existing XAML views for Xamarin.Forms applications.

## Why?

While we very much like the idea of Model-View-Update (aka "The Elm Architecture") and are fascinated by everything the folks behind Fabulous have achieved so far, we see scenarios where we would instead prefer to build mobile apps with Xamarin.iOS and Xamarin.Android instead of Xamarin.Forms. This has proven for us to be quite a reliable and enjoyable way of building high-quality mobile applications where we are able to share a lot of code and are still able to stay as close as possible to the bare metal.

## Constraints

Xamarin apps can be built in all languages that are supported by .NET – in theory. In practice, the Xamarin tooling, e.g., the parts that automatically generate the code for outlets and actions created in Xcode Interface Builder, has not been given much love for F# and has some severy bleeding edges. The focus is on C#, where tooling works excellent, even in third-party IDEs such as Jetbrains Rider!

Another caveat is that native UIs on both iOS and Android are heavily based on object-oriented concepts. So building them in a functional-first language such as F# would work – in theory. In practice, one would be spending much time dealing with things like inheritance, state-mutation, and eventually writing much ugly boiler-plate code.

In other words: We don't see F# shine here. Instead, we decided that it probably makes more sense to use for the UI what is supported best by the tooling and what is suited best for the applied concept: C#.

## It's a library, not a framework

We think of this as a library that is being used behind the scenes and across the apps. So a Fabulous.XamarinNative program will be hosted by things like a `UIViewController` or an `Activity`.

It will not deal with things like navigation and other tasks that are taken care of by heavy-weight frameworks like [MvvmCross](https://www.mvvmcross.com).
