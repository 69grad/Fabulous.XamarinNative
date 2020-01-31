using System;
using Fabulous.XamarinNative;

namespace CounterApp.iOS
{
    public partial class CounterViewController : UIFabulousViewController<Counter.Program>
    {
        public CounterViewController(IntPtr handle) : base(handle)
        {
        }
    }
}
