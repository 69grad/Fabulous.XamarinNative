using System;
using Fabulous.XamarinNative;
using Foundation;

namespace MultiPageApp.iOS
{
    public partial class AddPersonViewController
        : UIFabulousViewController<AddPersonStaticViewModel.AddPersonStaticViewModel>
    {
        public AddPersonViewController(IntPtr handle) : base(handle)
        {
        }

        partial void DismissViewModel(NSObject sender)
        {
            DismissViewController(true, null);
        }
    }
}
