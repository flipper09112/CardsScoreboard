using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsScoreboard.Models.Match
{
    public class Round
    {
        public int RoundNumber { get; set; }
        public List<MatchPlayerRound> Rounds { get; set; }
    }

    public class MatchPlayerRound
    {
        public int PlayerId { get; set; }
        public int PlayerPoints { get; set; }
    }
}