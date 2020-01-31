using System;
using Fabulous.XamarinNative;

namespace MultiPageApp.iOS
{
    public partial class PeopleViewController
        : UIFabulousTableViewController<PeopleStaticViewModel.PeopleStaticViewModel>
    {
        private Person[] _people;

        public Person[] People
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
    }
}
