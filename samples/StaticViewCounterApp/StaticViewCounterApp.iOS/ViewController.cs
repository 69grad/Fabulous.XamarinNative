using System;

namespace FabulousStaticViewTest.iOS
{
    public partial class ViewController : Fabulous.StaticView.UIFabulousViewController<StaticViewCounterApp.StaticViewCounterApp>
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }
    }
}
