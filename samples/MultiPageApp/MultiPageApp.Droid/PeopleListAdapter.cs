using System;
using Android.Support.V7.Widget;
using Android.Views;

namespace MultiPageApp.Droid
{
    public class PeopleListAdapter : RecyclerView.Adapter
    {
        private Person[] _people;

        public PeopleListAdapter(Person[] People)
        {
            _people = People;
        }

        public override int ItemCount => _people.Length;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (!(holder is PeopleListViewHolder peopleListViewHolder))
            {
                return;
            }

            peopleListViewHolder.NameTextView.Text = $"{_people[position].FirstName} {_people[position].LastName}";
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var layout = LayoutInflater.From(parent.Context).Inflate(Android.Resource.Layout.SimpleListItem1, parent, false);
            
            return new PeopleListViewHolder(layout);
        }
    }
}
