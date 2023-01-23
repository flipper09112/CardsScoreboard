using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CardsScoreboard.Models.General;
using CardsScoreboard.ViewModel.AddGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsScoreboard.Services.Interfaces
{
    public interface IGamesManagerService
    {
        Game GameSelected { get; set; }
        public int GamePlayersCount { get; }
        public List<Player> GamePlayers { get; }
        int GamePoints { get; set; }
        int QueensValue { get; set; }

        void AddPlayer(Player player);
        void CreateGame();
        bool NeedCreatePlayer();
        void SetPlayers(int playersCount, List<Player> playersSelected);
    }
}