using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.Models.General;
using CardsScoreboard.UI.Adapters.Home;
using CardsScoreboard.UI.ViewHolders.AddGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CardsScoreboard.UI.Adapters.AddGame
{
    public class GamesAdapter : RecyclerView.Adapter
    {
        private List<Game> games;
        public Game GameSelected;
        private ICommand _command;

        public GamesAdapter(List<Game> games, System.Windows.Input.ICommand command)
        {
            this.games = games;
            _command = command;
        }

        public override int ItemCount => games?.Count ?? 0;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is GameItemViewHolder gameItemViewHolder)
                gameItemViewHolder.Bind(games[holder.AbsoluteAdapterPosition], _command, GameSelected);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.GameItem, parent, false);
            var vh1 = new GameItemViewHolder(view);
            return vh1;
        }
        public override int GetItemViewType(int position)
        {
            return position;
        }
    }
}