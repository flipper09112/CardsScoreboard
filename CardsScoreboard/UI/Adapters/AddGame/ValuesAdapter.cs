using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.UI.ViewHolders.AddGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsScoreboard.UI.Adapters.AddGame
{
    public class ValuesAdapter : RecyclerView.Adapter
    {
        private List<int> spadesQueenValues;

        public ValuesAdapter(List<int> spadesQueenValues)
        {
            this.spadesQueenValues = spadesQueenValues;
        }

        public override int ItemCount => spadesQueenValues?.Count ?? 0;

        public int ValueSelected { get; internal set; }
        public EventHandler ItemClick { get; set; }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if(holder is ValueItemViewHolder valueItemVH)
            {
                valueItemVH.Bind(spadesQueenValues[holder.AbsoluteAdapterPosition], ValueSelected == spadesQueenValues[holder.AbsoluteAdapterPosition]);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ValueItem, parent, false);
            view.Click -= ViewClick;
            view.Click += ViewClick;
            var vh1 = new ValueItemViewHolder(view);
            return vh1;
        }

        private void ViewClick(object sender, EventArgs e)
        {
            ItemClick.Invoke(sender, e);
        }
    }
}