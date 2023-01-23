using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.UI.ViewHolders.Home;
using CardsScoreboard.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using static AndroidX.RecyclerView.Widget.RecyclerView;

namespace CardsScoreboard.UI.Adapters.Home
{
    public class HomePageAdapter : RecyclerView.Adapter
    {
        public ICommand OpenCurrentMatchCommmand { get; internal set; }

        private Models.Match.Match _currentMatch;
        private List<HomePageItem> _homePageItems;

        public HomePageAdapter(List<HomePageItem> homePageItems, Models.Match.Match currentMatch)
        {
            _currentMatch = currentMatch;
            this._homePageItems = homePageItems;
        }

        public override int ItemCount => _homePageItems?.Count ?? 0;


        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if(holder is CurrentGameViewHolder currentGameVH)
            {
                currentGameVH.Bind(_currentMatch, OpenCurrentMatchCommmand);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            switch (_homePageItems[viewType].HomePageItemType)
            {
                case HomePageItemTypeEnum.CurrentGame:
                    View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.CurrentGameVHItem, parent, false);
                    var currentGameViewHolder = new CurrentGameViewHolder(view);
                    return currentGameViewHolder;

                case HomePageItemTypeEnum.LastGame:
                    View view2 = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.CurrentGameVHItem, parent, false);
                    var vh = new CurrentGameViewHolder(view2);
                    return vh;

                case HomePageItemTypeEnum.AvaiableGames:
                    View view3 = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AvaiableGamesVHItem, parent, false);
                    var vh1 = new AvaiableGamesViewHolder(view3);
                    return vh1;
            }

            return null;
        }

        public override int GetItemViewType(int position)
        {
            return position;
        }
    }
}