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
    public class HeartsRoundValuesAdapter : RecyclerView.Adapter
    {
        private List<Player> _players;
        private List<MatchPlayerRound> rounds;
        private int _minValueLastRound;
        private int _maxValueLastRound;
        private bool _lastRound;

        public HeartsRoundValuesAdapter(List<MatchPlayerRound> rounds,
                                        List<Player> players,
                                        int minValueLastRound,
                                        int maxValueLastRound,
                                        bool lastRound)
        {
            _players = players;
            this.rounds = rounds;
            _minValueLastRound = minValueLastRound;
            _maxValueLastRound = maxValueLastRound;
            _lastRound = lastRound;
        }

        public override int ItemCount => rounds?.Count ?? 0;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if(holder is HeartsRoundValueItemViewHolder heartsRoundValueItemVH)
            {
                var player = _players[holder.AbsoluteAdapterPosition];
                var points = rounds.Find(item => item.PlayerId == player.Id).PlayerPoints;
                heartsRoundValueItemVH.Bind(points,
                                            _lastRound,
                                            _minValueLastRound,
                                            _maxValueLastRound);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.HeartsRoundValueItem, parent, false);
            var vh1 = new HeartsRoundValueItemViewHolder(view);
            return vh1;
        }
    }
}