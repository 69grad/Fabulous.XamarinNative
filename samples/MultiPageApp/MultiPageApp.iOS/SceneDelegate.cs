using Foundation;
using UIKit;

namespace MultiPageApp.iOS
{
    [Register(nameof(SceneDelegate))]
    public class SceneDelegate : UIResponder, IUIWindowSceneDelegate
    {
        [Export("window")]
        public UIWindow Window { get; set; }
    }
}
