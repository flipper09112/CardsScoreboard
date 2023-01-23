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

namespace CardsScoreboard.Constants
{
    public static class GamesConstants
    {
        public static List<Game> AllGames { get; internal set; } = new List<Game> 
        { 
            new Game()
            {
                Id = 1,
                Name = GameTypeEnum.Copas.ToString(),
                Image = GetImageGame(GameTypeEnum.Copas),
                AscendentPontuation = true,
                Active = true
            },
            new Game()
            {
                Id = 2,
                Name = GameTypeEnum.Sueca.ToString(),
                Image = GetImageGame(GameTypeEnum.Sueca),
                Active = false
            },
            new Game()
            {
                Id = 3,
                Name = GameTypeEnum.Buraco.ToString(),
                Image = GetImageGame(GameTypeEnum.Buraco),
                Active = false
            },
            new Game()
            {
                Id = 4,
                Name = GameTypeEnum.Canastra.ToString(),
                Image = GetImageGame(GameTypeEnum.Canastra),
                Active = false
            },
        };

        public static string GetImageGame(GameTypeEnum gameType)
        {
            switch (gameType)
            {
                case GameTypeEnum.Copas:
                    return "ic_heart";
                case GameTypeEnum.Sueca:
                    return "ic_sueca";
                case GameTypeEnum.Buraco:
                    return "ic_hole";
                case GameTypeEnum.Canastra:
                    return "ic_carroca";
                default:
                    return "heart_counter_logo";
            }
        }
    }
}