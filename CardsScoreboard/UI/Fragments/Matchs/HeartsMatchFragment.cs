using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.CardView.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.UI.Adapters.AddGame;
using CardsScoreboard.UI.Adapters.Match;
using CardsScoreboard.UI.Fragments.Base;
using CardsScoreboard.UI.Views;
using CardsScoreboard.ViewModel.Matchs;
using Google.Android.Material.BottomSheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CardsScoreboard.UI.Fragments.Matchs
{
    public class HeartsMatchFragment : BaseFragment<HeartsMatchViewModel>
    {
        private View _view;
        private RecyclerView _roundsRv;
        private RecyclerView _playersRv;
        private Button _addRoundButton;
        private MatchPlayersAdapter _playersAdapter;
        private HeartsRoundsAdapter _roundsAdapter;
        private AddRoundBottomSheetDialog bottomSheetDialog;

        protected override MainActivity MainActivity => (MainActivity)Activity;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (_view != null) return _view;

            // Inflate the layout for this fragment
            _view = inflater.Inflate(Resource.Layout.HeartsMatchFragment, container, false);

            _roundsRv = _view.FindViewById<RecyclerView>(Resource.Id.roundsRv);
            _playersRv = _view.FindViewById<RecyclerView>(Resource.Id.playersRv);
            _addRoundButton = _view.FindViewById<Button>(Resource.Id.addRoundButton);

            _playersRv.SetLayoutManager(new LinearLayoutManager(Context));
            _roundsRv.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Horizontal, false));

            return _view;
        }

        public override void SetUI()
        {
            MainActivity.HideNavBar();

            _addRoundButton.Text = ViewModel.AddRoundBtText;

            SetPlayersRv();
            SetRoundsRv();
        }

        private void SetRoundsRv()
        {
            _roundsAdapter = new HeartsRoundsAdapter(ViewModel.Rounds, ViewModel.Players);
            _roundsRv.SetAdapter(_roundsAdapter);
        }

        private void SetPlayersRv()
        {
            _playersAdapter = new MatchPlayersAdapter(ViewModel.Players);
            _playersRv.SetAdapter(_playersAdapter);
        }

        public override void SetupBindings()
        {
            _addRoundButton.Click += AddRoundButtonClick;
        }

        public override void CleanBindings()
        {
            _addRoundButton.Click -= AddRoundButtonClick;
        }

        protected override void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.ViewModelPropertyChanged(sender, e);

            switch(e.PropertyName)
            {
                case nameof(ViewModel.Rounds):
                    _roundsAdapter.Rounds = ViewModel.Rounds;
                    _roundsAdapter.NotifyDataSetChanged();
                    _roundsRv.ScrollToPosition(ViewModel.Rounds.Count - 1);
                    break;

                case nameof(ViewModel.NewRoundModel):
                    bottomSheetDialog?.UpdateList();
                    bottomSheetDialog?.SetButtonEnabled(ViewModel.AddRoundCommand.CanExecute(null));
                    break;
            }
        }

        private void AddRoundButtonClick(object sender, EventArgs e)
        {
            ShowBottomSheetDialog();
        }

        private void ShowBottomSheetDialog()
        {
            ViewModel.NewRoundModel = null;
            bottomSheetDialog = new AddRoundBottomSheetDialog(ViewModel.NewRoundModel, ViewModel.AddRoundCommand);
            bottomSheetDialog.Show(Activity.SupportFragmentManager, bottomSheetDialog.Tag);
        }
    }
}