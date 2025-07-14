
using Android.App;
using Android.OS;

namespace MixItUp.Droid
{
    [Activity(Theme = "@style/MainTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashAvtivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Create your application here
            StartActivity(typeof(MainActivity));

        }

    }
}