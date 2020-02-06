using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace MultiPageApp.Droid
{
    public class PeopleListViewHolder : RecyclerView.ViewHolder
    {
        public TextView NameTextView { get; }

        public PeopleListViewHolder(View itemView) : base(itemView)
        {
            NameTextView = itemView.FindViewById<TextView>(Android.Resource.Id.Text1);
        }
    }
}
