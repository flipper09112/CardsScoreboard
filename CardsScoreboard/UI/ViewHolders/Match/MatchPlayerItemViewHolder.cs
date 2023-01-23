using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.Models.General;
using Plugin.CurrentActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Java.Util.Jar.Attributes;

namespace CardsScoreboard.UI.ViewHolders.Match
{
    public class MatchPlayerItemViewHolder : RecyclerView.ViewHolder
    {
        private TextView _playerName;
        private ImageView _playerIcon;
        private View _divider;

        public MatchPlayerItemViewHolder(View itemView) : base(itemView)
        {
            _playerName = itemView.FindViewById<TextView>(Resource.Id.playerName);
            _playerIcon = itemView.FindViewById<ImageView>(Resource.Id.playerIcon);
            _divider = itemView.FindViewById(Resource.Id.divider);
        }

        internal void Bind(Player player, bool lastItem)
        {
            int imageResourceId = CrossCurrentActivity.Current.Activity.Resources.GetIdentifier(player.IconName, "drawable", CrossCurrentActivity.Current.Activity.PackageName);
            _playerIcon.SetImageResource(imageResourceId);

            _playerName.Text= player.Name;
            _divider.Visibility = lastItem ? ViewStates.Invisible : ViewStates.Visible;
        }
    }
}