using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.CardView.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.Helpers;
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
    public class HeartsMatchFragment : BaseFragment<HeartsMatchViewModel>, IDialogInterfaceOnClickListener
    {
        private View _view;
        private RecyclerView _roundsRv;
        private RecyclerView _playersRv;
        private Button _addRoundButton;
        private TextView _otherOptionsLabel;
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
            _otherOptionsLabel = _view.FindViewById<TextView>(Resource.Id.otherOptionsLabel);

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
            _otherOptionsLabel.Click += OtherOptionsLabelClick;
            ViewModel.ShowWinnerPage += ShowWinnerPage;
        }

        public override void CleanBindings()
        {
            _addRoundButton.Click -= AddRoundButtonClick;
            _otherOptionsLabel.Click -= OtherOptionsLabelClick;
            ViewModel.ShowWinnerPage -= ShowWinnerPage;
        }

        private void ShowWinnerPage(object sender, EventArgs e)
        {
            FragmentHelper.ShowFragment(this, new MatchWinnerFragment());
        }

        private void OtherOptionsLabelClick(object sender, EventArgs e)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(Context);
            builder.SetTitle("Other Options");

            builder.SetItems(new List<string>() { "Remove last round" }.ToArray(), this);

            // Mostrar o diálogo
            builder.Show();
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

        public void OnClick(IDialogInterface dialog, int which)
        {
            dialog.Dismiss();

            AlertDialog.Builder builder = new AlertDialog.Builder(Context);
            builder.SetTitle("Confirm action");
            builder.SetMessage("You have sure that want clear last round inserted?");

            builder.SetPositiveButton("Yes", (sender, e) =>
            {
                ViewModel.RemoveLastRoundCommand.Execute(null);
            });

            builder.SetNegativeButton("Cancel", (sender, e) =>
            {
                //Do nothing
            });

            // Mostrar o diálogo
            builder.Show();

        }
    }
}