using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace LineGame.Android
{
    [Activity(Label = "Line"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen"
        , AlwaysRetainTaskState = true
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Portrait
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize | ConfigChanges.ScreenLayout)]
    public class Activity : Microsoft.Xna.Framework.AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var g = new MainGame();
            SetContentView((View)g.Services.GetService(typeof(View)));
            g.Run();
        }
    }
}

