using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
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

            base.OnCreate(savedInstanceState);
        }
    }
}

