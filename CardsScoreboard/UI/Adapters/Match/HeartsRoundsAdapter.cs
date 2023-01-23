using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.Models.General;
using CardsScoreboard.Models.Match;
using CardsScoreboard.UI.ViewHolders.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsScoreboard.UI.Adapters.Match
{
    public class HeartsRoundsAdapter : RecyclerView.Adapter
    {
        public List<Round> Rounds { get; set; }
        private List<Player> _players;

        public HeartsRoundsAdapter(List<Round> rounds, List<Player> players)
        {
            Rounds = rounds;
            _players = players;
        }

        public override int ItemCount => Rounds?.Count ?? 0;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if(holder is HeartsRoundItemViewHolder roundItemVH)
            {
                roundItemVH.Bind(Rounds[holder.AbsoluteAdapterPosition], _players, holder.AbsoluteAdapterPosition == (ItemCount - 1) && holder.AbsoluteAdapterPosition != 0);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.HeartsRoundItem, parent, false);
            var vh1 = new HeartsRoundItemViewHolder(view);
            return vh1;
        }
    }
}