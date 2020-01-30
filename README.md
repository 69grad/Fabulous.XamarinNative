# Fabulous.XamarinNative

## What is this?

This is a **very early proof of concept**, intended to explore possible use cases for [Fabulous](https://github.com/fsprojects/Fabulous) running on Xamarin.iOS and Xamarin.Android. It is based on the "half-elmish approach" of [Fabulous.StaticView](https://github.com/fsprojects/Fabulous/tree/master/Fabulous.StaticView), which was built on top of existing XAML views for Xamarin.Forms applications.

## Why does it exist?

We think that Xamarin.Forms is nice for some scenarios, but building native apps with Xamarin.iOS and Xamarin.Android still has its advantages for certain areas as well. We also believe in [MVU](https://guide.elm-lang.org/architecture/) as the superior architecture approach compared to the ubiquitous [MVVM](https://en.wikipedia.org/wiki/Model–view–viewmodel). So we would like to have a Fabulous implementation for native Xamarin apps. Simple as that :-).

## Constraints

Xamarin apps can be built in all languages that are supported by .NET – in theory. In practice, the Xamarin tooling, e.g., the parts that automatically generate the code for outlets and actions created by Xcode Interface Builder, has not been given as much love for F# as it has been given for C#, and therefore suffers from some very bleeding edges. The focus of the Xamarin universe clearly is on C#, where tooling works excellent.

Another caveat is that native UIs on both iOS and Android are heavily based on object-oriented concepts. So building them in a functional-first language such as F# would work – in theory. In practice, one would be spending much time dealing with things like inheritance, mutating state, and eventually writing a lot of ugly boiler-plate code. 

In other words: We think that it makes sense to keep writing the UI stuff in C#.

## It's a library, not a framework

We think of this as a library that is being used behind the scenes and across the apps. So a Fabulous.XamarinNative program will be hosted by the "views" aka `UIViewControllers` and `Activities` and enable you to share common logic behind those platform-specific frontends. 

However, it will not deal with things like navigation and other tasks that are taken care of by heavy-weight frameworks like [MvvmCross](https://www.mvvmcross.com).

## Credits

This library would not be possible without the fabulous [Fabulous](https://github.com/fsprojects/Fabulous) library!
