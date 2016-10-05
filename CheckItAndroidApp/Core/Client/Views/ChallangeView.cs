using Android.App;
using Android.Widget;
using Android.OS;
using CheckItAndroidApp.Core.Data;
using Android.Support.V7.App;
using Android.Views;
using CheckItAndroidApp.Core.Business.Dtos;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using CheckItAndroidApp.Core.Business;
using Android.Content.Res;
using System;
using CheckItAndroidApp.Core.Data.Utils;

namespace CheckItAndroidApp.Client.Views
{
    [Activity(Label = "CheckIt", MainLauncher = false, Icon = "@drawable/icon")]
    public class ChallengeView : Activity
    {
        private PreferenceHelper prefHelper;
        private DataManger dataManager;
        private BiggerFloatingActionButton checkButton;
        private ChallengeDto challange;
        private int position;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            bundle = Intent.Extras;

            SetContentView(Resource.Layout.ViewChallenge);

            prefHelper = new PreferenceHelper(this);
            dataManager = new DataManger(this);
     
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            checkButton = FindViewById<BiggerFloatingActionButton>(Resource.Id.checkButton);
            var challengeName = FindViewById<TextView>(Resource.Id.challengeName);

            challange = new ChallengeDto
            {
                Id = bundle.GetInt("CHALLENGE_ID"),
                Name = bundle.GetString("NAME"),
                Duration = bundle.GetInt("DURATION"),
                EntriesCompleted = bundle.GetInt("ENTRIES_COMPLETED"),
                LastEntryDate = Utils.ToDateTimeNull(bundle.GetString("LAST_ENTRY_DATE"))
            };

            position = bundle.GetInt("POSITION");

            if (challange.CanCheck)
            {
                checkButton.BackgroundTintList = ColorStateList.ValueOf(Resources.GetColor(Resource.Color.colorPrimary));
                checkButton.Clickable = true;
                checkButton.Click += CheckButton_Click;
            }
            else
            {
                checkButton.BackgroundTintList = ColorStateList.ValueOf(Resources.GetColor(Resource.Color.lightGray));
                checkButton.Clickable = false;
            }

            challengeName.Text = challange.Name;

            SetActionBar(toolbar);

            ActionBar.Title = "Check It";
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            var entryDate = DateTime.Now;
            ////TODO preverit je potrebno, da se ta ekran ni odprt recimo 23:59:59 ker potem, bo po insertu namesto današnjega datuma, dalo jutrišnji datum.
            var isInserted = dataManager.ChallangeData.InsertChallengeEntry(challange.Id, entryDate);

            if (isInserted)
            {
                checkButton.BackgroundTintList = ColorStateList.ValueOf(Resources.GetColor(Resource.Color.lightGray));
                checkButton.Clickable = false;

                challange.EntriesCompleted++;

                Intent myIntent = new Intent(this, typeof(ChallengeListView));
                myIntent.PutExtra("CHALLENGE_ID", challange.Id);
                myIntent.PutExtra("ENTRY_DATE", entryDate.ToString(Utils.DateFormat));
                myIntent.PutExtra("POSITION", position);
                SetResult(Result.Ok, myIntent);
            }
        }

        /// <summary>
        /// This is the menu for the Toolbar/Action Bar to use
        /// </summary>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.ChallengeMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuSettings:
                    //do something
                    return true;
                case Android.Resource.Id.Home:
                    Finish();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}

//TODO bližnca za creat method stub
//TODO ctrl + click shortcut for go to implementation
//TODO  Tools -> Options -> Projects and Solutions -> General -> "Track Active Item in Solution Explorer" 
//TODO navigate to shorcut
//TODO get mad cash
//todo duplicate extension https://visualstudiogallery.msdn.microsoft.com/830a6482-3b8f-41a8-97b5-b9c581e5ad8b