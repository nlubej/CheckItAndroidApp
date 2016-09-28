using Android.App;
using Android.Widget;
using Android.OS;
using CheckItAndroidApp.Core.Data;
using Android.Support.V7.App;
using Android.Views;
using Android.Support.V7.Widget;
using CheckItAndroidApp.Core.Business.Adapters;
using Android.Support.Design.Widget;
using System.Collections.Generic;

namespace CheckItAndroidApp.Client.Views
{
    [Activity(Label = "CheckIt", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private RecyclerView mRecyclerView;
        private PreferenceHelper pref;
        private DataManger dm;


        List<Core.Business.Dtos.ChallangeDto> challenges = new List<Core.Business.Dtos.ChallangeDto>
            {
                new Core.Business.Dtos.ChallangeDto
                {
                    Duration = 10,
                    Name = "Name 1",
                    Id = 1
                },
                new Core.Business.Dtos.ChallangeDto
                {
                    Duration = 10,
                    Name = "Name 2",
                    Id = 2
                },
                new Core.Business.Dtos.ChallangeDto
                {
                    Duration = 10,
                    Name = "Name 3",
                    Id = 3
                },
                new Core.Business.Dtos.ChallangeDto
                {
                    Duration = 10,
                    Name = "Name 4",
                    Id = 4
                },
    };
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            pref = new PreferenceHelper(this);
            dm = new DataManger(this);

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            var toolbar = FindViewById<Android.Widget.Toolbar>(Resource.Id.toolbar);

            SetActionBar(toolbar);
            ActionBar.Title = "Check It";
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            var layoutManager = new LinearLayoutManager(this, LinearLayoutManager.Vertical, false);
            recyclerView.SetLayoutManager(layoutManager);

            var adapter = new ChallengeAdapter(challenges);

            recyclerView.SetAdapter(adapter);

            adapter.ItemClick += MoviesAdapter_ItemClick;

            // pref.Insert(PreferenceKeys.UserName, "Nejc Lubej");

            //  var userName = pref.GetString(PreferenceKeys.UserName);

            //Get Challanges from database
            //var challangeList = dm.ChallangeData.GetChallanges();


            //var toolbarBottom = FindViewById<Toolbar>(Resource.Id.toolbar_bottom);

            /*   toolbarBottom.Title = "Photo Editing";
               toolbarBottom.InflateMenu(Resource.Menu.photo_edit);
               toolbarBottom.MenuItemClick += (sender, e) => {
                   Toast.MakeText(this, "Bottom toolbar pressed: " + e.Item.TitleFormatted, ToastLength.Short).Show();
               };*/

        }

        private void MoviesAdapter_ItemClick(object sender, int e)
        {
            var linearLayout = this.FindViewById<LinearLayout>(Resource.Id.main_content);

            Toast.MakeText(this, "Top ActionBar pressed: " + challenges[e].Name.ToString(), ToastLength.Short).Show();
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
