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

namespace CardsScoreboard.Models.General
{
    public class Game
    {
        public int Id { get; set; }
        public GameTypeEnum GameTypeEnum { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool Active { get; internal set; }
        public bool AscendentPontuation { get; internal set; }
    }
    public enum GameTypeEnum
    {
        Copas,
        Sueca,
        Canastra,
        Buraco
    }
}