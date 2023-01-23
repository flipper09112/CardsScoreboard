using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.Models.General;
using CardsScoreboard.UI.ViewHolders.AddGame;
using CardsScoreboard.ViewModel.AddGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CardsScoreboard.UI.Adapters.AddGame
{
    public class PlayersAdapter : RecyclerView.Adapter
    {
        private List<Player> players;

        public PlayersAdapter(List<Player> players)
        {
            this.players = players;
        }

        public override int ItemCount => (players?.Count ?? 0) == 0 ? 1 : players.Count;

        public List<Player> PlayersSelected { get; internal set; }
        public ICommand SelectPlayer { get; internal set; }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if(holder is PlayerSelectionItemViewHolder playerSelectionItemVH)
            {
                playerSelectionItemVH.Bind(players[holder.AbsoluteAdapterPosition], 
                                           PlayersSelected.Contains(players[holder.AbsoluteAdapterPosition]),
                                           SelectPlayer);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if((players?.Count ?? 0) == 0)
            {
                View noPlayerView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.NoPlayerView, parent, false);
                var vh = new NoPlayerViewHolder(noPlayerView);
                return vh;
            }

            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.PlayerSelectionItem, parent, false);
            var vh1 = new PlayerSelectionItemViewHolder(view);
            return vh1;
        }
    }
}