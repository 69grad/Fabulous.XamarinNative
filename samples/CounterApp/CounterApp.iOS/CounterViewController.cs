using System;
using Fabulous.XamarinNative.iOS;

namespace CounterApp.iOS
{
    public partial class CounterViewController : UIFabulousViewController<CounterProgram>
    {
        public CounterViewController(IntPtr handle) : base(handle)
        {
        }
    }
}
