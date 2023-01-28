using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CardsScoreboard.Models.General;
using CardsScoreboard.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsScoreboard.Services.Interfaces
{
    public interface IMatchManagerService
    {
        Match MatchSelected { get; }

        void FinishGame(HeartsMatch match, Player winner, Player biggerLoser);
        void SetMatchSelected(Match match);
    }
}