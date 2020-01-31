using System;
using Fabulous.XamarinNative;
using Foundation;

namespace MultiPageApp.iOS
{
    public partial class PeopleFormViewController
        : FabulousUIViewController<PeopleFormProgram.Program>
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
