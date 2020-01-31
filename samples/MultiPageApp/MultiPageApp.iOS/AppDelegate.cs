using Fabulous.XamarinNative;
using Foundation;
using UIKit;

namespace MultiPageApp.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : UIResponder, IUIApplicationDelegate
    {
        [Export("window")]
        public UIWindow Window { get; set; }

        [Export("application:didFinishLaunchingWithOptions:")]
        public bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            FabulousSetup.initialize();

            return true;
        }
    }
}
