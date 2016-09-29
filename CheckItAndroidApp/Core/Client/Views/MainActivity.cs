using Android.App;
using Android.Widget;
using Android.OS;
using CheckItAndroidApp.Core.Data;
using Android.Support.V7.App;
using Android.Views;
using Android.Support.V7.Widget;
using CheckItAndroidApp.Core.Business.Adapters;
using System.Collections.Generic;
using CheckItAndroidApp.Core.Business.Dtos;

namespace CheckItAndroidApp.Client.Views
{
    [Activity(Label = "CheckIt", MainLauncher = true, Icon = "@drawable/icon")]
    public class ChallangeActivity : Activity
    {
        private PreferenceHelper prefHelper;
        private DataManger dataManager;
        private List<ChallangeDto> challenges;
           
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ViewChallenge);

            prefHelper = new PreferenceHelper(this);
            dataManager = new DataManger(this);

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            var toolbar = FindViewById<Android.Widget.Toolbar>(Resource.Id.toolbar);

            SetActionBar(toolbar);
            ActionBar.Title = "Check It";
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            var layoutManager = new LinearLayoutManager(this, LinearLayoutManager.Vertical, false);
            recyclerView.SetLayoutManager(layoutManager);

            //Get challenges from database
            challenges = dataManager.ChallangeData.GetChallanges();

            var adapter = new ChallengeAdapter(challenges);

            recyclerView.SetAdapter(adapter);

            adapter.ItemClick += MoviesAdapter_ItemClick;

            // pref.Insert(PreferenceKeys.UserName, "Nejc Lubej");

            //  var userName = pref.GetString(PreferenceKeys.UserName);
        }

        private void MoviesAdapter_ItemClick(object sender, int i)
        {
            var linearLayout = this.FindViewById<LinearLayout>(Resource.Id.main_content);

            Toast.MakeText(this, "Top ActionBar pressed: " + challenges[i].Name.ToString(), ToastLength.Short).Show();
        }

        /// <Docs>The options menu in which you place your items.</Docs>
		/// <returns>To be added.</returns>
		/// <summary>
		/// This is the menu for the Toolbar/Action Bar to use
		/// </summary>
		/// <param name="menu">Menu.</param>
		public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.home, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Toast.MakeText(this, "Top ActionBar pressed: " + item.TitleFormatted, ToastLength.Short).Show();
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