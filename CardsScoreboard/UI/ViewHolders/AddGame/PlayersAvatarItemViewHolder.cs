using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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
    public class PlayersAvatarItemViewHolder : RecyclerView.ViewHolder
    {
        private ICommand _selectAvatarCommand;
        private string _avatarItem;
        private ImageView _avatar;
        private ConstraintLayout _mainContainer;

        public PlayersAvatarItemViewHolder(View itemView) : base(itemView)
        {
            _avatar = itemView.FindViewById<ImageView>(Resource.Id.avatar);
            _mainContainer = itemView.FindViewById<ConstraintLayout>(Resource.Id.mainContainer);

            itemView.Click -= ItemViewClick;
            itemView.Click += ItemViewClick;
        }

        private void ItemViewClick(object sender, EventArgs e)
        {
            _selectAvatarCommand.Execute(_avatarItem);
        }

        internal void Bind(string avatar, bool selected, ICommand selectAvatarCommand)
        {
            _selectAvatarCommand = selectAvatarCommand;
            _avatarItem = avatar;


            int imageResourceId = CrossCurrentActivity.Current.Activity.Resources.GetIdentifier(avatar, "drawable", CrossCurrentActivity.Current.Activity.PackageName);
            _avatar.SetImageResource(imageResourceId);

            if(selected) 
            {
                _mainContainer.SetBackgroundResource(Resource.Color.selected_green);
            }
            else
            {
                _mainContainer.SetBackgroundDrawable(null);
            }

        }
    }
}