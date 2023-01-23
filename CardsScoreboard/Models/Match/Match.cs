using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CardsScoreboard.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsScoreboard.Models.Match
{
    public class Match
    {
        public Game GameSelected { get; protected set; }
        public int GamePoints { get; protected set; }
        public List<Player> GamePlayers { get; protected set; }
        public DateTime MatchCreatedDate { get; protected set; }
        public DateTime MatchEndedDate { get; protected set; }
        public List<Round> Rounds { get; protected set; }
    }

    public class HeartsMatch : Match
    {
        public int QueensValue { get; protected set; }

        public HeartsMatch(Game gameSelected, int gamePoints, int queensValue, List<Player> gamePlayers)
        {
            List<MatchPlayerRound> firstRound = new List<MatchPlayerRound>();
            gamePlayers.ForEach(player => firstRound.Add(new MatchPlayerRound()
            {
                PlayerId = player.Id,
                PlayerPoints = 0
            }));

            Rounds = new List<Round>()
            {
                new Round()
                {
                    RoundNumber = 0,
                    Rounds = firstRound
                }
            };

            MatchCreatedDate = DateTime.Now;
            GameSelected = gameSelected;
            GamePoints = gamePoints;
            QueensValue = queensValue;
            GamePlayers = gamePlayers;
        }
    }
}