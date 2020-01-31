using System;
using Fabulous.XamarinNative.iOS;
using MultiPageApp.iOS.ViewController;

namespace MultiPageApp.iOS
{
    public partial class PeopleViewController : UIFabulousTableViewController<PeopleStaticViewModel.PeopleStaticViewModel>
    {
        private Common.Person[] _people;

        public Common.Person[] People
        {
            get => _people;
            set
            {
                _people = value;
                TableView.DataSource = new PeopleTableViewDataSource(_people);
                TableView.ReloadData();
            }
        }

        public PeopleViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
