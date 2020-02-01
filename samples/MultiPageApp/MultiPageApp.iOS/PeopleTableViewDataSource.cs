using System;
using Foundation;
using UIKit;

namespace MultiPageApp.iOS
{
    public class PeopleTableViewDataSource : UITableViewDataSource
    {
        private readonly Person[] _people;

        public PeopleTableViewDataSource(Person[] people)
        {
            _people = people;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _people?.Length ?? 0;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("PersonTableViewCell")
                       ?? new UITableViewCell(UITableViewCellStyle.Default, "PersonTableViewCell")
                       {
                           Accessory = UITableViewCellAccessory.DisclosureIndicator
                       };

            var person = _people[indexPath.Row];

            cell.TextLabel.Text = $"{person.FirstName} {person.LastName}";
            return cell;
        }
    }
}
