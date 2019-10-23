using System;
using Fabulous.StaticView;
using Foundation;

namespace MultiPageApp.iOS
{
    public partial class AddPersonViewController : UIFabulousViewController<AddPersonStaticViewModel.AddPersonStaticViewModel>
    {
        public AddPersonViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void DismissViewModel(NSObject sender)
        {
            DismissViewController(true, null);
        }
    }
}
