using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace MultiPageApp.Droid
{
    public class PeopleListViewHolder : RecyclerView.ViewHolder
    {
        public TextView Text { get; }

        public PeopleListViewHolder(View itemView) : base(itemView)
        {
            Text = itemView.FindViewById<TextView>(Android.Resource.Id.Text1);
        }
    }
}
