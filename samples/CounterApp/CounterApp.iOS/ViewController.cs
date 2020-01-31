using System;
using Fabulous.XamarinNative.iOS;

namespace FabulousStaticViewTest.iOS
{
    public partial class ViewController : UIFabulousViewController<CounterApp.StaticViewCounterApp>
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }
    }
}
