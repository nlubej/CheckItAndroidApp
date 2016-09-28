using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using CheckItAndroidApp.Core.Business.Dtos;

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

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var movieViewHolder = (ChallengeViewHolder)holder;
            movieViewHolder.ChallengeNameTextView.Text = challanges[position].Name;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var layout = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ChallengeRow, parent, false);

            return new ChallengeViewHolder(layout, OnItemClick);
        }

        public override int ItemCount
        {
            get { return challanges.Count; }
        }

        void OnItemClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }

        public class ChallengeViewHolder : RecyclerView.ViewHolder
        {
            public TextView ChallengeNameTextView { get; set; }

            public ChallengeViewHolder(View itemView, Action<int> listener) : base(itemView)
            {
                ChallengeNameTextView = itemView.FindViewById<TextView>(Resource.Id.challengeName);

                itemView.Click += (s, e) => listener(Position);
            }
        }
    
}