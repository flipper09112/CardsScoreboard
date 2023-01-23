using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CardsScoreboard.Models.General;
using CardsScoreboard.Models.Match;
using CardsScoreboard.Services.Interfaces;
using CardsScoreboard.ViewModel.AddGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsScoreboard.Services
{
    public class GamesManagerService : IGamesManagerService
    {
        private IMatchManagerService _matchManagerService;

        public Game GameSelected { get; set; }
        public int GamePlayersCount { get; private set; }
        public List<Player> GamePlayers { get; private set; }
        public int GamePoints { get; set; }
        public int QueensValue { get; set; }

        public GamesManagerService(IMatchManagerService matchManagerService) 
        {
            _matchManagerService = matchManagerService;
        }

        public void AddPlayer(Player player)
        {
            GamePlayers.Add(player);
        }

        public void CreateGame()
        {
            switch (GameSelected.GameTypeEnum)
            {
                case GameTypeEnum.Copas:
                    CreateHeartsGame();
                    break;
            } 
        }

        public bool NeedCreatePlayer()
        {
            return GamePlayers.Count < GamePlayersCount;
        }

        public void SetPlayers(int playersCount, List<Player> playersSelected)
        {
            GamePlayersCount = playersCount;
            GamePlayers = playersSelected;
        }

        #region GAMES CREATORS

        private void CreateHeartsGame()
        {
            Match match = new HeartsMatch(GameSelected, GamePoints, QueensValue, GamePlayers);
            _matchManagerService.SetMatchSelected(match);
        }

        #endregion
    }
}