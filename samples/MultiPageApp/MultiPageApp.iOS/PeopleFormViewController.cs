using System;
using Fabulous.XamarinNative;
using Foundation;

namespace MultiPageApp.iOS
{
    public partial class PeopleFormViewController
        : UIFabulousViewController<PeopleFormProgram.Program>
    {
        public PeopleFormViewController(IntPtr handle) : base(handle)
        {
        }

        partial void DismissViewModel(NSObject sender)
        {
            DismissViewController(true, null);
        }
    }
}
