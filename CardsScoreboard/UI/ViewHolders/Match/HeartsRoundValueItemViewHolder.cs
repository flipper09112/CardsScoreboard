using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using AndroidX.RecyclerView.Widget;
using Plugin.CurrentActivity;
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

        internal void Bind(int value, bool lastRound, int minValueLastRound, int maxValueLastRound)
        {
            _valueTv.Text = value.ToString();

            if (lastRound)
            {
                int color = value == minValueLastRound ? Resource.Color.selected_green : value == maxValueLastRound ? Resource.Color.gradient_3 : Resource.Color.titlePageTextColor;
                _valueTv.SetTextColor(CrossCurrentActivity.Current.Activity.Resources.GetColor(color));
            }
            else
            {
                _valueTv.SetTextColor(CrossCurrentActivity.Current.Activity.Resources.GetColor(Resource.Color.titlePageTextColor));
            }
        }
    }
}