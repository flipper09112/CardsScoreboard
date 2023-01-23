using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CardsScoreboard.Models.General;
using CardsScoreboard.Models.Match;
using CardsScoreboard.Services.Interfaces;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CardsScoreboard.ViewModel.Matchs
{
    public class HeartsMatchViewModel : BaseViewModel
    {
        private IMatchManagerService _matchManagerService;

        public List<Player> Players => _matchManagerService.MatchSelected.GamePlayers;
        public List<Round> Rounds => _matchManagerService.MatchSelected.Rounds;
        public HeartsMatch Match => _matchManagerService.MatchSelected as HeartsMatch;

        public EventHandler ShowWinnerPage;

        public string AddRoundBtText { get; internal set; }
        public ICommand AddRoundCommand { get; }

        public HeartsMatchViewModel(IMatchManagerService matchManagerService)
        {
            _matchManagerService = matchManagerService;

            AddRoundBtText = "Add round";

            AddRoundCommand = new DelegateCommand(AddRound, CanAddRound);
        }

        private List<NewRoundModel> _newRoundModel;
        public List<NewRoundModel> NewRoundModel
        {
            get
            {
                if (_newRoundModel == null)
                {
                    _newRoundModel = Players.Select(item => new NewRoundModel() { Player = item,
                                                                                  AddPointCommand = AddPointAction, 
                                                                                  MinusPointCommand = MinusPointAction,
                                                                                  SelectQueenCommand = SelectQueenAction
                    }).ToList();
                }

                return _newRoundModel;
            }
            set
            {
                _newRoundModel = value;
            }
        }

        private void AddRound()
        {
            List<MatchPlayerRound> listPoints = new List<MatchPlayerRound>();

            foreach (var newRoundItem in NewRoundModel)
            {
                listPoints.Add(new MatchPlayerRound()
                {
                    PlayerId = newRoundItem.Player.Id,
                    PlayerPoints = Rounds[Rounds.Count - 1].Rounds.Find(item => item.PlayerId == newRoundItem.Player.Id).PlayerPoints + GetTotalPoints(newRoundItem)
                });
            }

            Rounds.Add(new Round()
            {
                RoundNumber = Rounds.Count,
                Rounds = listPoints
            });

            if (listPoints.Any(item => item.PlayerPoints >= Match.GamePoints))
            {
                ShowWinnerPage?.Invoke(null, null);
            }

            RaisePropertyChanged(nameof(Rounds));
        }

        private int GetTotalPoints(NewRoundModel newRoundItem)
        {
            return (newRoundItem.HasQueen ? ((HeartsMatch)_matchManagerService.MatchSelected).QueensValue : 0) + newRoundItem.HeartsPoints;
        }

        private bool CanAddRound()
        {
            return NewRoundModel.Any(item => item.HasQueen);
        }

        private void SelectQueenAction(NewRoundModel newRoundModel, bool selected)
        {
            NewRoundModel.ForEach(item => item.HasQueen = false);
            newRoundModel.HasQueen = selected;
            RaisePropertyChanged(nameof(NewRoundModel));
        }

        private void AddPointAction(NewRoundModel newRoundModel)
        {
            newRoundModel.HeartsPoints += 1;
            RaisePropertyChanged(nameof(NewRoundModel));
        }

        private void MinusPointAction(NewRoundModel newRoundModel)
        {
            if (newRoundModel.HeartsPoints == 0) return;

            newRoundModel.HeartsPoints += 1;
            RaisePropertyChanged(nameof(NewRoundModel));
        }
        

        public override void Appearing()
        {
        }

        public override void DisAppearing()
        {
        }
    }

    public class NewRoundModel
    {
        public Player Player { get; set; }
        public bool HasQueen { get; set; }
        public int HeartsPoints { get; set; }
        public Action<NewRoundModel> AddPointCommand { get; internal set; }
        public Action<NewRoundModel> MinusPointCommand { get; internal set; }
        public Action<NewRoundModel, bool> SelectQueenCommand { get; internal set; }
    }
}