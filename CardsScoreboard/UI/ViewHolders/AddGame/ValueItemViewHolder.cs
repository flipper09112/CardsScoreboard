using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.CardView.Widget;
using AndroidX.Core.Content;
using AndroidX.RecyclerView.Widget;
using Plugin.CurrentActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsScoreboard.UI.ViewHolders.AddGame
{
    public class ValueItemViewHolder : RecyclerView.ViewHolder
    {
        private CardView _cardview;
        private TextView _valueTv;

        public ValueItemViewHolder(View itemView) : base(itemView)
        {
            _cardview = itemView.FindViewById<CardView>(Resource.Id.cardview);
            _valueTv = itemView.FindViewById<TextView>(Resource.Id.valueTv);
        }

        internal void Bind(int value, bool selected)
        {
            _valueTv.Text = value.ToString();

            ColorStateList colorStateList = ContextCompat.GetColorStateList(CrossCurrentActivity.Current.Activity, selected ? Resource.Color.selected_green : Resource.Color.gradient_1);
            _cardview.CardBackgroundColor = colorStateList;
        }
    }
}