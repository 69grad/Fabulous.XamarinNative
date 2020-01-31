using Foundation;
using UIKit;

namespace CounterApp.iOS
{
    [Register(nameof(SceneDelegate))]
    public class SceneDelegate : UIResponder, IUIWindowSceneDelegate
    {
        [Export("window")]
        public UIWindow Window { get; set; }
    }
}
