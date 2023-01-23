using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.Models.General;
using CardsScoreboard.ViewModel.AddGame;
using Com.Airbnb.Lottie;
using Plugin.CurrentActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CardsScoreboard.UI.ViewHolders.AddGame
{
    public class PlayerSelectionItemViewHolder : RecyclerView.ViewHolder
    {
        private ImageView _avatarIcon;
        private TextView _playerName;
        private LottieAnimationView _checkBox;
        private ICommand _selectPlayerCommand;
        private Player _player;

        public PlayerSelectionItemViewHolder(View itemView) : base(itemView)
        {
            _avatarIcon = itemView.FindViewById<ImageView>(Resource.Id.avatarIcon);
            _playerName = itemView.FindViewById<TextView>(Resource.Id.playerName);
            _checkBox = itemView.FindViewById<LottieAnimationView>(Resource.Id.checkBox);

            itemView.Click -= ItemViewClick;
            itemView.Click += ItemViewClick;
        }

        private void ItemViewClick(object sender, EventArgs e)
        {
            _selectPlayerCommand.Execute(_player);
        }

        internal void Bind(Player player, bool selected, ICommand selectPlayer)
        {
            _selectPlayerCommand = selectPlayer;
            _player = player;

            _playerName.Text = player.Name;
            _checkBox.Frame = selected ? 100 : 0;

            int imageResourceId = CrossCurrentActivity.Current.Activity.Resources.GetIdentifier(player.IconName, "drawable", CrossCurrentActivity.Current.Activity.PackageName);
            _avatarIcon.SetImageResource(imageResourceId);
        }
    }
}