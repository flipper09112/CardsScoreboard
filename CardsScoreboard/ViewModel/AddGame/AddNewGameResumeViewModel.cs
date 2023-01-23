using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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
    public class AddNewGameResumeViewModel : BaseViewModel
    {
        private IGamesManagerService _gamesManagerService;

        public string Description { get; internal set; }
        public string PageTitle { get; internal set; }
        public string GameName { get; internal set; }
        public string PlayersNumber { get; internal set; }
        public string GamePoints { get; internal set; }
        public string OtherValue { get; internal set; }
        public string GameImage { get; internal set; }
        public string OtherCardImage { get; internal set; }

        public EventHandler ShowMatchPage;
        public ICommand CreateGameCommand { get; }

        public AddNewGameResumeViewModel(IGamesManagerService gamesManagerService)
        {
            _gamesManagerService = gamesManagerService;

            PageTitle = "Resume";
            Description = "Check if new game has all settings that you want";

            GameName = _gamesManagerService.GameSelected.Name;
            GameImage = _gamesManagerService.GameSelected.Image;
            PlayersNumber = _gamesManagerService.GamePlayersCount.ToString();
            GamePoints = _gamesManagerService.GamePoints.ToString();
            OtherValue = GetOtherValue();
            OtherCardImage = GetOtherCardImage();

            CreateGameCommand = new DelegateCommand(CreateGame);
        }

        private void CreateGame()
        {
            _gamesManagerService.CreateGame();
            ShowMatchPage?.Invoke(null, null);
        }

        private string GetOtherCardImage()
        {
            return "ic_spades_queen";
        }

        private string GetOtherValue()
        {
            return _gamesManagerService.QueensValue.ToString();
        }

        public override void Appearing()
        {
        }

        public override void DisAppearing()
        {
        }
    }
}