using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using CheckItAndroidApp.Core.Business.Dtos;
using System.Linq;
using Android.Graphics;
using Android.Content;

namespace CheckItAndroidApp.Core.Business.Adapters
{
    public class ChallengeAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        private Context context;

        private List<ChallengeDto> challenges;

        public ChallengeAdapter(Context context, List<ChallengeDto> challanges)
        {
            this.context = context;
            this.challenges = challanges;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int pos)
        {
            var holder = (ChallengeViewHolder)viewHolder;

            var daysToGo = challenges[pos].Duration - challenges[pos].EntriesCompleted;

            holder.ChallengeName.Text = challenges[pos].Name;
            holder.ChallengeDuration.Text = string.Format("{0} {1} to go", daysToGo, (daysToGo == 1) ? "day" : "days");
       
            if (challenges[pos].CanCheck)
            {
                holder.ChallengeStatus.SetImageResource(Resource.Drawable.CircleFull);
            }
            else 
            {
                holder.ChallengeStatus.SetImageResource(Resource.Drawable.CircleFull);
                holder.ChallengeStatus.SetColorFilter(context.Resources.GetColor(Resource.Color.lightGray));
                holder.ChallengeName.SetTextColor(context.Resources.GetColor(Resource.Color.lightGray));
                holder.ChallengeDuration.SetTextColor(context.Resources.GetColor(Resource.Color.lightGray));
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var layout = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.RowChallenge, parent, false);

            return new ChallengeViewHolder(layout, OnItemClick);
        }

        public override int ItemCount
        {
            get { return challenges.Count; }
        }

        void OnItemClick(int position)
        {
            ItemClick?.Invoke(this, position);
        }

        public void AddEntryCount(int position, DateTime entryDate)
        {
            var challenge = challenges[position];

            if (challenge == null)
                return;

            challenge.EntriesCompleted++;
            challenge.LastEntryDate = entryDate;

            if (challenge.IsCompleted)
                NotifyItemRemoved(position);
            else
                NotifyItemChanged(position);
        }

        public void AddNewChallenge(ChallengeDto challenge)
        {
            challenges.Add(challenge);
            NotifyDataSetChanged();
        }

        internal void UpdateChallenge(ChallengeDto challenge)
        {
            throw new NotImplementedException();
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