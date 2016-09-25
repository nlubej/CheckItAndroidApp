using Android.App;
using Android.Widget;
using Android.OS;
using CheckItAndroidApp.Core.Data;

namespace CheckItAndroidApp.Client.Views
{
    [Activity(Label = "CheckIt", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private PreferenceHelper pref;
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            pref = new PreferenceHelper(this);
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            pref.Insert(PreferenceHelper.UserName, "Matic Lubej");

            var userName = pref.GetString(PreferenceHelper.UserName);
            button.Text = string.Format("{0}, click me!", userName);

            button.Click += delegate
            {
                button.Text = string.Format("{0} {1} clicks!", count++, GetString(Resource.String.SecretString));
            };
        }
    }
}

//TODO bližnca za creat method stub
//TODO ctrl + click shortcut for go to implementation
//TODO  Tools -> Options -> Projects and Solutions -> General -> "Track Active Item in Solution Explorer" 
//TODO navigate to shorcut
