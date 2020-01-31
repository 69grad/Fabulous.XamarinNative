using Fabulous.XamarinNative;
using Foundation;
using UIKit;

namespace CounterApp.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : UIResponder, IUIApplicationDelegate
    {
        public UIWindow Window { get; set; }

        [Export("application:didFinishLaunchingWithOptions:")]
        public bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            FabulousIosSetup.initialize();

            return true;
        }

        [Export("application:configurationForConnectingSceneSession:options:")]
        public UISceneConfiguration GetConfiguration(
            UIApplication application,
            UISceneSession connectingSceneSession,
            UISceneConnectionOptions options
        )
        {
            return UISceneConfiguration.Create(
                "Default Configuration",
                connectingSceneSession.Role
            );
        }
    }
}
