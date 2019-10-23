using System;
using UIKit;

namespace MultiPageApp.iOS.ViewController
{
    public class PeopleTableViewDelegate : UITableViewDelegate
    {
        private readonly PeopleViewController _viewController;

        public PeopleTableViewDelegate(PeopleViewController viewController)
        {
            _viewController = viewController;
        }
    }
}
