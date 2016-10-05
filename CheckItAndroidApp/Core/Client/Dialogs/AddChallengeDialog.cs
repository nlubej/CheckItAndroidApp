using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FR.Ganfra.Materialspinner;
using CheckItAndroidApp.Core.Client.Callbacks;
using CheckItAndroidApp.Core.Business.Dtos;
using CheckItAndroidApp.Core.Business;
using CheckItAndroidApp.Core.Data;

namespace CheckItAndroidApp.Core.Client.Dialogs
{
    [Activity(Label = "AddChallengeDialog")]
    public class AddChallengeDialog : DialogFragment
    {
        private enum DaysOption
        {
            Custom,
            Manual
        }
        private List<Frequency> frequencies;
        private LinearLayout manualLayout;
        private MaterialSpinner spinner;
        private Switch switchOption;
        private EditText challengeName;
        private EditText days;
        private ImageButton selectDateBtn;
        private TextView durationText;
        private SeekBar seekBarDuration;
        private EditText dateEditText;
        private DataManger dataManager;

        public event EventHandler<ChallengeDto> ChallangeAdded;
        public event EventHandler<ChallengeDto> ChallengeUpdated;

        public static AddChallengeDialog NewInstance()
        {
            var dialogFragment = new AddChallengeDialog();
            return dialogFragment;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Begin building a new dialog.
            var builder = new AlertDialog.Builder(Activity);

            //Get the layout inflater
            var inflater = Activity.LayoutInflater;



            //Inflate the layout for this dialog
            var dialogView = inflater.Inflate(Resource.Layout.DialogAddNewChallenge, null);
            dataManager = new DataManger(dialogView.Context);

            frequencies = dataManager.ChallangeData.GetPredefinedFrequencies();


            var adapter = new ArrayAdapter<Frequency>(dialogView.Context, Android.Resource.Layout.SimpleSpinnerItem, frequencies);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner = dialogView.FindViewById<MaterialSpinner>(Resource.Id.spinner);
            manualLayout = dialogView.FindViewById<LinearLayout>(Resource.Id.manualLayout);
            switchOption = dialogView.FindViewById<Switch>(Resource.Id.switchOption);
            var yesBtn = dialogView.FindViewById<Button>(Resource.Id.yesBtn);
            var noBtn = dialogView.FindViewById<Button>(Resource.Id.noBtn);
            days = dialogView.FindViewById<EditText>(Resource.Id.days);
            seekBarDuration = dialogView.FindViewById<SeekBar>(Resource.Id.seekBarDuration);
            durationText = dialogView.FindViewById<TextView>(Resource.Id.durationText);
            challengeName = dialogView.FindViewById<EditText>(Resource.Id.challengeName);
            selectDateBtn = dialogView.FindViewById<ImageButton>(Resource.Id.pickDateBtn);
            dateEditText = dialogView.FindViewById<EditText>(Resource.Id.dateEditText);




            seekBarDuration.Max = 100 - 1;
            spinner.Adapter = adapter;

            switchOption.CheckedChange += SwitchOption_CheckedChange;
            yesBtn.Click += YesBtn_Click;
            noBtn.Click += NoBtn_Click;
            seekBarDuration.ProgressChanged += SeekBarDuration_ProgressChanged;
            selectDateBtn.Click += _dateSelectButton_Click;

            manualLayout.Visibility = ViewStates.Gone;

            builder.SetView(dialogView);
            var dialog = builder.Create();

            return dialog;
        }

        private void SeekBarDuration_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            var progress = e.Progress + 1;
            if (progress == 1)
            {
                durationText.Text = string.Format("One time thing");
            }
            else if (e.SeekBar.Max == progress - 1)
            {
                durationText.Text = string.Format("Foreeweeeeeer");
            }
            else
            {
                durationText.Text = string.Format("{0} days", progress);
            }
        }

        private void _dateSelectButton_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                dateEditText.Text = time.ToString("d. MMMM yyyy");
            });

            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void NoBtn_Click(object sender, EventArgs e)
        {
            Dismiss();
        }

        private void YesBtn_Click(object sender, EventArgs e)
        {
            Dialog.Dismiss();

            var result = new ChallengeDto
            {
                Name = challengeName.Text,
                Duration = seekBarDuration.Progress,
                Frequency = new Frequency
                {
                    Value = (switchOption.Checked) ? Convert.ToInt32(days.Text) :  frequencies[spinner.SelectedItemPosition].Id,
                    Type = (switchOption.Checked) ? Data.Utils.Enums.FrequencyType.Custom : Data.Utils.Enums.FrequencyType.Predefined,
                }
            };

            dataManager.ChallangeData.InsertChallenge(result);

            ChallangeAdded?.Invoke(this, result);
        }

        private void SwitchOption_CheckedChange(object sender, EventArgs e)
        {
            if(switchOption.Checked)
            {
                manualLayout.Visibility = ViewStates.Visible;
                spinner.Visibility = ViewStates.Invisible;
            }
            else
            {
                manualLayout.Visibility = ViewStates.Gone;
                spinner.Visibility = ViewStates.Visible;
            }
        }
    }
}