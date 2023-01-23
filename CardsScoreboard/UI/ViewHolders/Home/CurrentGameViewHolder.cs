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
using System.Windows.Input;

namespace CardsScoreboard.UI.ViewHolders.Home
{
    public class CurrentGameViewHolder : RecyclerView.ViewHolder
    {
        private ImageView _gameIcon;
        private TextView _gameNameLabel;
        private TextView _gameStartedLabel;
        private TextView _gamePointsLabel;
        private ICommand _openCurrentMatchCommmand;

        public CurrentGameViewHolder(View itemView) : base(itemView)
        {
            _gameIcon = itemView.FindViewById<ImageView>(Resource.Id.gameIcon);
            _gameNameLabel = itemView.FindViewById<TextView>(Resource.Id.gameNameLabel);
            _gameStartedLabel = itemView.FindViewById<TextView>(Resource.Id.gameStartedLabel);
            _gamePointsLabel = itemView.FindViewById<TextView>(Resource.Id.gamePointsLabel);

            itemView.Click -= ItemViewClick;
            itemView.Click += ItemViewClick;
        }

        private void ItemViewClick(object sender, EventArgs e)
        {
            _openCurrentMatchCommmand.Execute(null);
        }

        internal void Bind(Models.Match.Match currentMatch, ICommand openCurrentMatchCommmand)
        {
            _openCurrentMatchCommmand = openCurrentMatchCommmand;

            _gameNameLabel.Text = currentMatch.GameSelected.Name;
            _gamePointsLabel.Text = currentMatch.GamePoints.ToString();
            _gameStartedLabel.Text = currentMatch.MatchCreatedDate.ToString("dd/MM/yyyy HH:mm");

            int imageResourceId = CrossCurrentActivity.Current.Activity.Resources.GetIdentifier(currentMatch.GameSelected.Image, "drawable", CrossCurrentActivity.Current.Activity.PackageName);
            _gameIcon.SetImageResource(imageResourceId);
        }
    }
}