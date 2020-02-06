using Android.App;
using Android.OS;
using Android.Widget;
using Fabulous.XamarinNative;

namespace MultiPageApp.Droid
{
    [Activity(Label = "PeopleFormActivity")]
    public class PeopleFormActivity : FabulousActivity<PeopleFormProgram.Program>
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.PeopleFormLayout);
            var button = FindViewById<Button>(Resource.Id._saveButton);
            button.Click += (sender, e) => Finish();

            base.OnCreate(savedInstanceState);
        }
    }
}
