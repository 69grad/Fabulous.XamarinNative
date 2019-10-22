// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace FabulousStaticViewTest.iOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        UIKit.UITextField _twoWayOneTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton _decrementButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton _incrementButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton _resetButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField _twoWayFirstTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField _twoWaySecondTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel _valueLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (_decrementButton != null) {
                _decrementButton.Dispose ();
                _decrementButton = null;
            }

            if (_incrementButton != null) {
                _incrementButton.Dispose ();
                _incrementButton = null;
            }

            if (_resetButton != null) {
                _resetButton.Dispose ();
                _resetButton = null;
            }

            if (_twoWayFirstTextField != null) {
                _twoWayFirstTextField.Dispose ();
                _twoWayFirstTextField = null;
            }

            if (_twoWaySecondTextField != null) {
                _twoWaySecondTextField.Dispose ();
                _twoWaySecondTextField = null;
            }

            if (_valueLabel != null) {
                _valueLabel.Dispose ();
                _valueLabel = null;
            }
        }
    }
}