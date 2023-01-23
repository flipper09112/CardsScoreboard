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
    public class Player
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public string IconName { get; internal set; }
    }
}