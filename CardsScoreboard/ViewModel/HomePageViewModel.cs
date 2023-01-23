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
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CardsScoreboard.ViewModel
{
    public class HomePageViewModel : BaseViewModel
    {
        private IMatchManagerService _matchManagerService;

        public List<HomePageItem> HomePageItems { get; internal set; }
        public Match CurrentMatch => _matchManagerService.MatchSelected;

        public EventHandler ShowHeartsMatchPage;

        public ICommand OpenCurrentMatchCommmand { get; }

        public HomePageViewModel(IMatchManagerService matchManagerService)
        {
            _matchManagerService = matchManagerService;

            OpenCurrentMatchCommmand = new DelegateCommand(OpenCurrentMatch);
        }

        private void OpenCurrentMatch()
        {
            ShowHeartsMatchPage?.Invoke(null, null);
        }

        public override void Appearing()
        {
            if(HomePageItems == null)
                HomePageItems = new List<HomePageItem>()
                {
                    new HomePageItem()
                    {
                        HomePageItemType = HomePageItemTypeEnum.CurrentGame
                    },
                    /*new HomePageItem()
                    {
                        HomePageItemType = HomePageItemTypeEnum.LastGame
                    },*/
                    new HomePageItem()
                    {
                        HomePageItemType = HomePageItemTypeEnum.AvaiableGames
                    },
                };
        }

        public override void DisAppearing()
        {
        }
    }

    public class HomePageItem
    {
        public string Title { get; set; }

        public HomePageItemTypeEnum HomePageItemType { get; set; }
    }

    public enum HomePageItemTypeEnum
    {
        LastGame,
        CurrentGame,
        AvaiableGames
    }
}