using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using CheckItAndroidApp.Core.Business.Dtos;
using Android.Content;

namespace CheckItAndroidApp.Core.Business.Adapters
{
    public class EntryAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        private Context context;

        private List<ChallengeEntryDto> entries;

        public EntryAdapter(Context context, List<ChallengeEntryDto> entries)
        {
            this.context = context;
            this.entries = entries;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int pos)
        {
            var holder = (ChallengeViewHolder)viewHolder;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var layout = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.RowEntry, parent, false);

            return new ChallengeViewHolder(layout, OnItemClick);
        }

        public override int GetItemViewType(int position)
        {
            if(position == entries.Count)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public override int ItemCount
        {
            get { return entries.Count; }
        }

        void OnItemClick(int position)
        {
            ItemClick?.Invoke(this, position);
        }

        internal void AddEntryCount(int v1, string v2)
        {
            throw new NotImplementedException();
        }
    }

    public class ChallengeEntryViewHolder : RecyclerView.ViewHolder
    {
        public TextView EntrySequence { get; set; }
        public TextView EntryDate { get; set; }
        public TextView EntryTime { get; set; }

        public ChallengeEntryViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            EntrySequence = itemView.FindViewById<TextView>(Resource.Id.entrySequence);
            EntryDate = itemView.FindViewById<TextView>(Resource.Id.entryDate);
            EntryTime = itemView.FindViewById<TextView>(Resource.Id.entryTime);

            itemView.Click += (s, e) => listener(LayoutPosition);
        }
    }
}