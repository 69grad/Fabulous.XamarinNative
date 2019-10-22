using System;
using Fabulous.Core;

namespace FabulousStaticViewTest.iOS
{
    public partial class ViewController : StaticViewController.UIFabulousViewController<StaticViewCounterApp.StaticViewCounterApp>
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }
    }
}
