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
using System.Windows.Input;

namespace CardsScoreboard.UI.Adapters.AddGame
{
    public class PlayersAvataresAdapter : RecyclerView.Adapter
    {
        private List<string> avatares;

        public PlayersAvataresAdapter(List<string> avatares)
        {
            this.avatares = avatares;
        }

        public override int ItemCount => avatares?.Count ?? 0;

        public string SelectedAvatar { get; internal set; }
        public ICommand SelectAvatarCommand { get; internal set; }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if(holder is PlayersAvatarItemViewHolder playersAvatarItemVH)
            {
                playersAvatarItemVH.Bind(avatares[holder.AbsoluteAdapterPosition], SelectedAvatar == avatares[holder.AbsoluteAdapterPosition], SelectAvatarCommand);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.PlayersAvatarItem, parent, false);
            var vh1 = new PlayersAvatarItemViewHolder(view);
            return vh1;
        }
    }
}