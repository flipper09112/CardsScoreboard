using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CardsScoreboard.Models.General;
using CardsScoreboard.Services;
using CardsScoreboard.Services.Interfaces;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CardsScoreboard.ViewModel.AddGame
{
    public class ChoosePlayersViewModel : BaseViewModel
    {
        private IGamesManagerService _gamesManagerService;

        public string StepPosition { get; internal set; }
        public string Title { get; internal set; }
        public string Description { get; internal set; }

        public EventHandler CreatePlayerPage;
        public EventHandler ChooseQueenValuePage;
        public ICommand ContinueCommand { get; }
        public ICommand SelectPlayerCommand { get; }
        public ICommand SelectPlayerCountCommand { get; }

        public List<Player> Players { get; internal set; }
        public List<Player> PlayersSelected { get; internal set; } = new List<Player>();
        public int PlayersCount { get; private set; } = 1;

        public ChoosePlayersViewModel(IGamesManagerService gamesManagerService)
        {
            _gamesManagerService = gamesManagerService;

            StepPosition = string.Empty;
            Title = "How many players do you want?";
            Description = "Choose quantity of players possible";

            ContinueCommand = new DelegateCommand(Continue, CanContinue);
            SelectPlayerCommand = new DelegateCommand<Player>(SelectPlayer);
            SelectPlayerCountCommand = new DelegateCommand<object>(SelectPlayersCount);
        }

        private void SelectPlayersCount(object playersCount)
        {
            PlayersCount = (int)playersCount;
            RaisePropertyChanged(nameof(PlayersCount));
        }

        private void SelectPlayer(Player player)
        {
            if(PlayersSelected.Contains(player))
                PlayersSelected.Remove(player);
            else
                PlayersSelected.Add(player);

            RaisePropertyChanged(nameof(PlayersSelected));
        }

        private bool CanContinue()
        {
            return PlayersSelected.Count <= PlayersCount;
        }

        private void Continue()
        {
            _gamesManagerService.SetPlayers(PlayersCount, PlayersSelected);

            if(_gamesManagerService.NeedCreatePlayer())
            {
                CreatePlayerPage?.Invoke(null, null);
            }
            else if(_gamesManagerService.GameSelected.GameTypeEnum == Models.General.GameTypeEnum.Copas)
            {
                ChooseQueenValuePage?.Invoke(null, null);
            }
        }

        public override void Appearing()
        {
            Players = new List<Player>()
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
            };
        }

        public override void DisAppearing()
        {
        }
    }

   
}