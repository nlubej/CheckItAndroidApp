using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using CheckItAndroidApp.Core.Business.Dtos;
using System.Linq;

namespace CheckItAndroidApp.Core.Business.Adapters
{
    public class ChallengeAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;

        private readonly List<ChallangeDto> challanges;

        public ChallengeAdapter(List<ChallangeDto> challanges)
        {
            this.challanges = challanges;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int pos)
        {
            var holder = (ChallengeViewHolder)viewHolder;

            holder.ChallengeName.Text = challanges[pos].Name;
            holder.ChallengeDuration.Text = string.Format("{0} days to go", challanges[pos].Duration - challanges[pos].EntriesCompleted);

            var lastDate = challanges[pos].LastEntryDate;

            if (challanges[pos].EntriesCompleted == 0  || 
                (challanges[pos].EntriesCompleted != challanges[pos].Duration  &&
                 lastDate.HasValue && DateTime.Today > lastDate.Value.Date))
            {
                holder.ChallengeStatus.SetImageResource(Resource.Drawable.CircleFull);
            }
            else
            {
                holder.ChallengeStatus.SetImageResource(Resource.Drawable.CircleEmpty);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var layout = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.RowChallenge, parent, false);

            return new ChallengeViewHolder(layout, OnItemClick);
        }

        public override int ItemCount
        {
            get { return challanges.Count; }
        }

        void OnItemClick(int position)
        {
            ItemClick?.Invoke(this, position);
        }

        public void SubtractEntryCount(int challengeId)
        {
            var challenge = challanges.FirstOrDefault(w => w.Id == challengeId);

            if (challenge == null)
                return;

            challenge.EntriesCompleted++;
        }
    }

    public class ChallengeViewHolder : RecyclerView.ViewHolder
    {
        public TextView ChallengeName { get; set; }
        public TextView ChallengeDuration { get; set; }
        public ImageView ChallengeStatus { get; set; }

        public ChallengeViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            ChallengeName = itemView.FindViewById<TextView>(Resource.Id.challengeName);
            ChallengeDuration = itemView.FindViewById<TextView>(Resource.Id.challengeDuration);
            ChallengeStatus = itemView.FindViewById<ImageView>(Resource.Id.challengeStatus);

            itemView.Click += (s, e) => listener(LayoutPosition);
        }
    }
}