using Android.App;
using Android.OS;
using Fabulous.XamarinNative;

namespace CounterApp.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : FabulousActivity<CounterProgram.Program>
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.activity_main);
            FabulousSetup.initialize();

            base.OnCreate(savedInstanceState);
        }
    }
}

