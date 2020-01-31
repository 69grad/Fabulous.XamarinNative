// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MultiPageApp.iOS
{
    [Register ("AddPersonViewController")]
    partial class AddPersonViewController
    {
        [Outlet]
        UIKit.UITextField _firstnameTextField { get; set; }


        [Outlet]
        UIKit.UITextField _lastnameTextField { get; set; }


        [Outlet]
        UIKit.UIButton _saveButton { get; set; }


        [Action ("DismissViewModel:")]
        partial void DismissViewModel (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
        }
    }
}