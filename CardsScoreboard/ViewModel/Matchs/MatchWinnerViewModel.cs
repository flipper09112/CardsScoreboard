using CardsScoreboard.Models.General;
using CardsScoreboard.Services.Implementations;
using CardsScoreboard.Services.Interfaces;
using System;
namespace CardsScoreboard.ViewModel.Matchs
{
	public class MatchWinnerViewModel : BaseViewModel
    {
        private IMatchManagerService _matchManagerService;

        public Player Winner => _matchManagerService.MatchSelected.Winner;
        public Player BiggerLoser => _matchManagerService.MatchSelected.BiggerLoser;

        public MatchWinnerViewModel(IMatchManagerService matchManagerService)
		{
            _matchManagerService = matchManagerService;
        }

        public override void Appearing()
        {
        }

        public override void DisAppearing()
        {
        }
    }
}

