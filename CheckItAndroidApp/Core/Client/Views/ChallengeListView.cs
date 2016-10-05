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
using Android.Content;
using Android.Runtime;
using System.Linq;
using Android.Support.Design.Widget;
using CheckItAndroidApp.Core.Data.Utils;
using CheckItAndroidApp.Core.Client.Dialogs;
using CheckItAndroidApp.Core.Client.Callbacks;
using System;

namespace CheckItAndroidApp.Client.Views
{
    [Activity(Label = "CheckIt", MainLauncher = true, Icon = "@drawable/icon")]
    public class ChallengeListView : Activity
    {
        private PreferenceHelper prefHelper;
        private DataManger dataManager;
        private List<ChallengeDto> challenges;
        private ChallengeAdapter adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ViewChallengeList);

            prefHelper = new PreferenceHelper(this);
            dataManager = new DataManger(this);

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            var toolbar = FindViewById<Android.Widget.Toolbar>(Resource.Id.toolbar);
            var addItemButton = FindViewById<com.refractored.fab.FloatingActionButton>(Resource.Id.addItemButton);

            SetActionBar(toolbar);
            ActionBar.Title = "Check It";
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            var layoutManager = new LinearLayoutManager(this, LinearLayoutManager.Vertical, false);
            recyclerView.SetLayoutManager(layoutManager);

            //Get challenges from database
            challenges = dataManager.ChallangeData.GetChallanges();

            adapter = new ChallengeAdapter(this, challenges.Where(w => !w.IsCompleted).ToList());

            recyclerView.SetAdapter(adapter);

            adapter.ItemClick += Challenge_ItemClick;
            addItemButton.Click += AddItemButton_Click;
        }

        private void AddItemButton_Click(object sender, System.EventArgs e)
        {

            var transaction = FragmentManager.BeginTransaction();
            var dialogFragment = new AddChallengeDialog();
            dialogFragment.ChallangeAdded += OnChallengeAdded;
            dialogFragment.ChallengeUpdated += OnChallengeUpdated;
            dialogFragment.Show(transaction, "dialog_fragment");


            //AddChallengeDialog dialog = AddChallengeDialog.NewInstance();
           // dialog.Show(FragmentManager, "AddChallengeDialog");
        }

        private void Challenge_ItemClick(object sender, int i)
        {
            Intent intent = new Intent(this, typeof(ChallengeView));
            intent.PutExtra("NAME", challenges[i].Name);

            Bundle bundle = new Bundle();
            bundle.PutInt("CHALLENGE_ID", challenges[i].Id);
            bundle.PutString("NAME", challenges[i].Name);
            bundle.PutInt("DURATION", challenges[i].Duration);
            bundle.PutInt("ENTRIES_COMPLETED", challenges[i].EntriesCompleted);
            bundle.PutString("LAST_ENTRY_DATE", challenges[i].LastEntryDate.HasValue ? challenges[i].LastEntryDate.Value.ToString(Utils.DateFormat) : null);
            bundle.PutInt("POSITION", i);

            intent.PutExtras(bundle);
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                var challengeId = data.GetIntExtra("CHALLENGE_ID", -1);
                var entryDate = Utils.ToDateTime(data.GetStringExtra("ENTRY_DATE"));
                var position = data.GetIntExtra("POSITION", -1);

                adapter.AddEntryCount(position, entryDate);
            }
        }

        /// <Docs>The options menu in which you place your items.</Docs>
		/// <returns>To be added.</returns>
		/// <summary>
		/// This is the menu for the Toolbar/Action Bar to use
		/// </summary>
		/// <param name="menu">Menu.</param>
		public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.ChallengeListMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuSettings:

                    var linearLayout = FindViewById<FrameLayout>(Resource.Id.challengeListLayout);
                    Snackbar.Make(linearLayout, "This is a simple snackbar.", Snackbar.LengthLong)
                            .SetAction("OK", action => { })
                            .Show();

                    //do something
                    return true;
                case Resource.Id.home:
                    Finish();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void OnChallengeAdded(object sender, ChallengeDto challenge)
        {
            adapter.AddNewChallenge(challenge);
        }

        private void OnChallengeUpdated(object sender, ChallengeDto challenge)
        {
            adapter.UpdateChallenge(challenge);
        }
    }
}

//MATIC TODO

//TODO bližnca za creat method stub
//TODO ctrl + click shortcut for go to implementation
//TODO  Tools -> Options -> Projects and Solutions -> General -> "Track Active Item in Solution Explorer" 
//TODO navigate to shorcut
//TODO get mad cash
//todo duplicate extension https://visualstudiogallery.msdn.microsoft.com/830a6482-3b8f-41a8-97b5-b9c581e5ad8b



// IDEAS TODO
//setting -> Return after button is pressed ?