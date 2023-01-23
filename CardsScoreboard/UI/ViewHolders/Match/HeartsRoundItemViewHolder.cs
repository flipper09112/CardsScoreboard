using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.Models.General;
using CardsScoreboard.Models.Match;
using CardsScoreboard.UI.Adapters.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsScoreboard.UI.ViewHolders.Match
{
    public class HeartsRoundItemViewHolder : RecyclerView.ViewHolder
    {
        private TextView _roundNumber;
        private RecyclerView _roundValuesRv;
        private HeartsRoundValuesAdapter _adapter;

        public HeartsRoundItemViewHolder(View itemView) : base(itemView)
        {
            _roundNumber = itemView.FindViewById<TextView>(Resource.Id.roundNumber);
            _roundValuesRv = itemView.FindViewById<RecyclerView>(Resource.Id.roundValuesRv);

            _roundValuesRv.SetLayoutManager(new LinearLayoutManager(itemView.Context));
        }

        internal void Bind(Round round, List<Player> players, bool lastRound)
        {
            _roundNumber.Text = "#" + round.RoundNumber;

            _adapter = new HeartsRoundValuesAdapter(round.Rounds,
                                                    players,
                                                    round.Rounds.Min(item => item.PlayerPoints),
                                                    round.Rounds.Max(item => item.PlayerPoints),
                                                    lastRound);
            _roundValuesRv.SetAdapter(_adapter);
        }
    }
}