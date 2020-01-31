using System;
using Fabulous.XamarinNative;

namespace MultiPageApp.iOS
{
    public partial class PeopleListViewController
        : FabulousUITableViewController<PeopleListProgram.Program>
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

        public PeopleListViewController(IntPtr handle) : base(handle)
        {
        }
    }
}
