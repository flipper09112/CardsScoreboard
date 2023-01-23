using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsScoreboard.UI.ViewHolders.Match
{
    public class HeartsRoundValueItemViewHolder : RecyclerView.ViewHolder
    {
        private TextView _valueTv;

        public HeartsRoundValueItemViewHolder(View itemView) : base(itemView)
        {
            _valueTv = itemView.FindViewById<TextView>(Resource.Id.valueTv);
        }

        internal void Bind(int value)
        {
            _valueTv.Text = value.ToString();
        }
    }
}