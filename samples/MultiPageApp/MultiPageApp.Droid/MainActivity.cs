using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Fabulous.XamarinNative;

namespace MultiPageApp.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : FabulousActivity<PeopleListProgram.Program>
    {
        private RecyclerView _recyclerView;

        private Person[] _people;

        public Person[] People
        {
            get => _people;
            set
            {
                _people = value;

                _recyclerView.SetAdapter(new PeopleListAdapter(_people));
                _recyclerView.SetLayoutManager(new LinearLayoutManager(this));
                _recyclerView.GetAdapter().NotifyDataSetChanged();
            }
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            FabulousSetup.initialize();

            SetContentView(Resource.Layout.activity_main);
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.List);

            var toolBar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);

            base.OnCreate(savedInstanceState);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var item = menu.Add(0, 0, 0, "Add");
            item.SetShowAsAction(ShowAsAction.Always);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            StartActivity(typeof(PeopleFormActivity));

            return base.OnOptionsItemSelected(item);
        }
    }
}

