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
using static Android.Icu.Text.CaseMap;
using static Android.Util.EventLogTags;

namespace CardsScoreboard.ViewModel.AddGame.Hearts
{
    public class SelectSpadesQueenValueViewModel : BaseViewModel
    {
        private IGamesManagerService _gamesManagerService;

        public string StepPosition { get; internal set; }
        public string Description { get; internal set; }
        public string Title { get; internal set; }

        public EventHandler ChooseEndGamePointsPage;
        public ICommand ContinueCommand { get; }
        public ICommand SelecteValueCommand { get; }
        public int SpadesQueenValue { get; private set; } = 10;

        public List<int> SpadesQueenValues = new List<int>()
        {
            5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20
        };

        public SelectSpadesQueenValueViewModel(IGamesManagerService gamesManagerService)
        {
            _gamesManagerService = gamesManagerService;

            StepPosition = string.Empty;
            Title = "How much is the queen of spades worth?";
            Description = "Select the value of the queen of spades";

            ContinueCommand = new DelegateCommand(Continue, CanContinue);
            SelecteValueCommand = new DelegateCommand<object>(SelecteValue);
        }

        private void SelecteValue(object value)
        {
            SpadesQueenValue = SpadesQueenValues[(int)value];
            RaisePropertyChanged(nameof(SpadesQueenValue));
        }

        private void Continue()
        {
            _gamesManagerService.QueensValue = SpadesQueenValue;
            ChooseEndGamePointsPage?.Invoke(null, null);
        }

        private bool CanContinue()
        {
            return true;
        }

        public override void Appearing()
        {
        }

        public override void DisAppearing()
        {
        }
    }
}