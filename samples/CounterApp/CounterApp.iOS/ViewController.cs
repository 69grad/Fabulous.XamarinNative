using System;

namespace FabulousStaticViewTest.iOS
{
    public partial class ViewController : Fabulous.StaticView.UIFabulousViewController<CounterApp.StaticViewCounterApp>
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }
    }
}
