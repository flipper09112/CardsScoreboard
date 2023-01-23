using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CardsScoreboard.Constants;
using CardsScoreboard.Models.General;
using CardsScoreboard.Models.Match;
using CardsScoreboard.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsScoreboard.Services.Implementations
{
    public class MatchManagerService : IMatchManagerService
    {
        public Match MatchSelected { get; private set; } = new HeartsMatch(GamesConstants.AllGames[0], 100, 10, new List<Player>()
        {
            new Player()
            {
                Id = 1,
                Name = "Filipe",
                IconName = "ic_avatar_1"
            },
            new Player()
            {
                Id = 2,
                Name = "Filipe 2",
                IconName = "ic_avatar_2"
            },
            new Player()
            {
                Id = 3,
                Name = "Filipe 3",
                IconName = "ic_avatar_3"
            },
        });

        public void SetMatchSelected(Match match)
        {
            MatchSelected = match;
        }
    }
}