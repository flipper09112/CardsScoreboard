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
using static Android.Icu.Text.CaseMap;
using static Android.Renderscripts.Sampler;
using static Android.Util.EventLogTags;

namespace CardsScoreboard.ViewModel.AddGame
{
    public class ChooseEndGamePointsViewModel : BaseViewModel
    {
        private IGamesManagerService _gamesManagerService;

        public string StepPosition { get; }
        public string Title { get; }
        public string Description { get; }

        public EventHandler ShowNewGameResumePage;
        public ICommand ContinueCommand { get; }
        public ICommand SelecteMaxPointsValueCommand { get; }

        public int MaxPointsValue { get; private set; } = 100;

        public List<int> MaxPointsValues { get; private set; } = Enumerable.Range(30, 200).ToList();

        public ChooseEndGamePointsViewModel(IGamesManagerService gamesManagerService)
        {
            _gamesManagerService = gamesManagerService;

            StepPosition = string.Empty;
            Title = "When do you want the game to end?";
            Description = "Choose max points of the game";

            ContinueCommand = new DelegateCommand(Continue, CanContinue);
            SelecteMaxPointsValueCommand = new DelegateCommand<object>(SelecteValue);
        }

        private void SelecteValue(object value)
        {
            MaxPointsValue = MaxPointsValues[(int)value];
            RaisePropertyChanged(nameof(MaxPointsValue));
        }

        private bool CanContinue()
        {
            return true;
        }

        private void Continue()
        {
            _gamesManagerService.GamePoints = MaxPointsValue;
            ShowNewGameResumePage?.Invoke(this, EventArgs.Empty);
        }

        public override void Appearing()
        {
        }

        public override void DisAppearing()
        {
        }
    }
}