using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CardsScoreboard.Constants;
using CardsScoreboard.Models.General;
using CardsScoreboard.Services.Interfaces;
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
    public class CreatePlayerViewModel : BaseViewModel
    {
        private IGamesManagerService _gamesManagerService;

        public string StepPosition { get; }
        public string Title { get; }
        public string Description { get; }

        public EventHandler CreatePlayerPage;
        public EventHandler ChooseQueenValuePage;

        public ICommand ContinueCommand { get; }
        public ICommand SelectPlayerAvatarCommand { get; }
        public ICommand DefinePlayerNameCommand { get; }
        
        public List<string> Avatares { get; internal set; } = AvataresConstants.Avatares;
        public string SelectedAvatar { get; private set; }
        public string PlayerName { get; private set; }

        public CreatePlayerViewModel(IGamesManagerService gamesManagerService) 
        {
            _gamesManagerService = gamesManagerService;

            StepPosition = string.Empty;
            Title = "Create new player";
            Description = "Give all information needed for create a new player";

            ContinueCommand = new DelegateCommand(Continue, CanContinue);
            SelectPlayerAvatarCommand = new DelegateCommand<string>(SelectPlayerAvatar);
            DefinePlayerNameCommand = new DelegateCommand<string>(DefinePlayerName);
        }

        private void DefinePlayerName(string name)
        {
            PlayerName = name;
            RaisePropertyChanged(nameof(PlayerName));
        }

        private void SelectPlayerAvatar(string avatar)
        {
            SelectedAvatar = avatar;
            RaisePropertyChanged(nameof(SelectedAvatar));
        }

        private bool CanContinue()
        {
            return !string.IsNullOrEmpty(PlayerName) && SelectedAvatar != null;
        }

        private void Continue()
        {
            _gamesManagerService.AddPlayer(new Player()
            {
                Name = PlayerName,
                IconName = SelectedAvatar
            });

            if (_gamesManagerService.NeedCreatePlayer())
            {
                CreatePlayerPage?.Invoke(null, null);
            }
            else if (_gamesManagerService.GameSelected.GameTypeEnum == Models.General.GameTypeEnum.Copas)
            {
                ChooseQueenValuePage?.Invoke(null, null);
            }
        }

        public override void Appearing()
        {
        }

        public override void DisAppearing()
        {
        }
    }
}