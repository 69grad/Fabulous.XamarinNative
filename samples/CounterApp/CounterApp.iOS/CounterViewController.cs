using System;
using Fabulous.XamarinNative;

namespace CounterApp.iOS
{
    public partial class CounterViewController : UIFabulousViewController<CounterProgram>
    {
        public CounterViewController(IntPtr handle) : base(handle)
        {
        }
    }
}
