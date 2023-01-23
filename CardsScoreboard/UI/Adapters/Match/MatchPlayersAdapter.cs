using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.Models.General;
using CardsScoreboard.UI.ViewHolders.AddGame;
using CardsScoreboard.UI.ViewHolders.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsScoreboard.UI.Adapters.Match
{
    public class MatchPlayersAdapter : RecyclerView.Adapter
    {
        private List<Player> players;

        public MatchPlayersAdapter(List<Player> players)
        {
            this.players = players;
        }

        public override int ItemCount => players?.Count ?? 0;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is MatchPlayerItemViewHolder matchPlayerItemVH)
                matchPlayerItemVH.Bind(players[holder.AbsoluteAdapterPosition], (holder.AbsoluteAdapterPosition + 1) == players.Count);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.MatchPlayerItem, parent, false);
            var vh1 = new MatchPlayerItemViewHolder(view);
            return vh1;
        }
    }
}