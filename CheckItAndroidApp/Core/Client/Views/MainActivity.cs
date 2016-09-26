using Android.App;
using Android.Widget;
using Android.OS;
using CheckItAndroidApp.Core.Data;
using CheckItAndroidApp.Core.Data.Utils;

namespace CheckItAndroidApp.Client.Views
{
    [Activity(Label = "CheckIt", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private PreferenceHelper pref;
        private int count = 1;
        private DataManger dm;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            pref = new PreferenceHelper(this);
            dm = new DataManger(this);

            Button button = FindViewById<Button>(Resource.Id.MyButton);

            pref.Insert(PreferenceKeys.UserName, "Nejc Lubej");

            var userName = pref.GetString(PreferenceKeys.UserName);
            button.Text = string.Format("{0}, click me!", userName);

            button.Click += delegate
            {
                button.Text = string.Format("{0} {1} clicks!", count++, GetString(Resource.String.SecretString));
            };

            //Get Challanges from database
            var challangeList = dm.ChallangeData.GetChallanges();
        }
    }
}

//TODO bližnca za creat method stub
//TODO ctrl + click shortcut for go to implementation
//TODO  Tools -> Options -> Projects and Solutions -> General -> "Track Active Item in Solution Explorer" 
//TODO navigate to shorcut
//TODO get mad cash
