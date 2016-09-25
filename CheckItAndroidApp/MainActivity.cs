using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Linq;

namespace CheckItAndroidApp
{
    [Activity(Label = "CheckIt", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            //Check it
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it

            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate
            {
                button.Text = string.Format("{0} {1} clicks!", count++, "{1}");
            };

        }

        private bool GetMethod(int v)
        {
            throw new NotImplementedException();
        }
    }
}

