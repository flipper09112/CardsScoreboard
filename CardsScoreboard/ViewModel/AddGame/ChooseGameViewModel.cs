using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CardsScoreboard.Constants;
using CardsScoreboard.Models.General;
using CardsScoreboard.Services;
using CardsScoreboard.Services.Interfaces;
using CardsScoreboard.UI.Fragments.Base;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using static Android.Icu.Text.CaseMap;
using static Android.Util.EventLogTags;

namespace CardsScoreboard.ViewModel.AddGame
{
    public class ChooseGameViewModel : BaseViewModel
    {
        private IGamesManagerService _gamesManagerService;

        public string StepPosition { get; internal set; }
        public string Description { get; internal set; }
        public string Title { get; internal set; }
        public List<Game> Items { get; private set; }

        public EventHandler NextStepPage;

        public ICommand SelectGameCommand { get; }
        public ICommand ContinueCommand { get; }

        public ChooseGameViewModel(IGamesManagerService gamesManagerService)
        {
            _gamesManagerService = gamesManagerService;

            StepPosition = string.Empty;
            Title = "What game do you want?";
            Description = "Choose one game for start scoreboard";

            SelectGameCommand = new DelegateCommand<Game>(SelectGame);
            ContinueCommand = new DelegateCommand(Continue, CanContinue);
        }

        private void Continue()
        {
            _gamesManagerService.GameSelected = GameSelected;
            NextStepPage?.Invoke(null, null);
        }

        private bool CanContinue()
        {
            return GameSelected != null;
        }

        private void SelectGame(Game game)
        {
            if (GameSelected != null && game.Id == GameSelected.Id)
            {
                GameSelected = null;
            }
            else
            {
                GameSelected = game;
            }
            RaisePropertyChanged(nameof(GameSelected));
        }


        public Game _gameSelected;
        public Game GameSelected
        {
            get => _gameSelected;
            set {
                _gameSelected = value;
                RaisePropertyChanged(nameof(GameSelected));
            }
        }

        public override void Appearing()
        {
            Items = GamesConstants.AllGames;
        }

        public override void DisAppearing()
        {
        }
    }
}