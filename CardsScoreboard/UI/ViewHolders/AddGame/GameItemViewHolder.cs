using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.CardView.Widget;
using AndroidX.ConstraintLayout.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.Models.General;
using Plugin.CurrentActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CardsScoreboard.UI.ViewHolders.AddGame
{
    public class GameItemViewHolder : RecyclerView.ViewHolder
    {
        private ImageView _gameIcon;
        private TextView _gameName;
        private CardView _cardContainer;
        private ConstraintLayout _constraintLayoutContainer;
        private ImageView _checkIcon;
        private ICommand _command;
        private Game _game;

        public GameItemViewHolder(View itemView) : base(itemView)
        {
            _gameIcon = itemView.FindViewById<ImageView>(Resource.Id.gameIcon);
            _gameName = itemView.FindViewById<TextView>(Resource.Id.gameName);
            _cardContainer = itemView.FindViewById<CardView>(Resource.Id.cardContainer);
            _constraintLayoutContainer = itemView.FindViewById<ConstraintLayout>(Resource.Id.constraintLayoutContainer);
            _checkIcon = itemView.FindViewById<ImageView>(Resource.Id.checkIcon);

            itemView.Click -= ItemViewClick;
            itemView.Click += ItemViewClick;
        }

        private void ItemViewClick(object sender, EventArgs e)
        {
            _command?.Execute(_game);
        }

        internal void Bind(Game game, ICommand command, Game gameSelected)
        {
            _command = command;
            _game = game;

            _gameName.Text = game.Name;

            int imageResourceId = CrossCurrentActivity.Current.Activity.Resources.GetIdentifier(game.Image, "drawable", CrossCurrentActivity.Current.Activity.PackageName);
            _gameIcon.SetImageResource(imageResourceId);

            _cardContainer.Alpha = game.Active ? 1 : 0.3f;

            _checkIcon.Visibility = game.Id == gameSelected?.Id ? ViewStates.Visible : ViewStates.Invisible;

            if (game.Id == gameSelected?.Id)
                _constraintLayoutContainer.SetBackgroundResource(Resource.Drawable.container_15dp);
            else
                _constraintLayoutContainer.SetBackgroundDrawable(null);
        }
    }
}