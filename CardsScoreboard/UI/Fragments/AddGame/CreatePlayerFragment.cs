using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.Helpers;
using CardsScoreboard.UI.Adapters.AddGame;
using CardsScoreboard.UI.Fragments.Base;
using CardsScoreboard.UI.Views;
using CardsScoreboard.ViewModel.AddGame;
using CardsScoreboard.ViewModel.AddGame.Hearts;
using Google.Android.Material.ProgressIndicator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CardsScoreboard.UI.Fragments.AddGame
{
    public class CreatePlayerFragment : BaseFragment<CreatePlayerViewModel>
    {
        private TextView _stepPositionTv;
        private TextView _pageQuestionTv;
        private TextView _pageDescriptionTv;
        private ImageView _continueButton;
        private ImageView _backIcon;
        private RecyclerView _mainRv;
        private CircularProgressIndicator _circleIndicator;
        private EditText _editTextTextPersonName;
        private PlayersAvataresAdapter _playersAvataresAdapter;

        protected override MainActivity MainActivity => (MainActivity)Activity;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Inflate the layout for this fragment
            View view = inflater.Inflate(Resource.Layout.CreatePlayerFragment, container, false);

            _stepPositionTv = view.FindViewById<TextView>(Resource.Id.stepPositionTv);
            _pageQuestionTv = view.FindViewById<TextView>(Resource.Id.pageQuestionTv);
            _pageDescriptionTv = view.FindViewById<TextView>(Resource.Id.pageDescriptionTv);
            _continueButton = view.FindViewById<ImageView>(Resource.Id.continueButton);
            _backIcon = view.FindViewById<ImageView>(Resource.Id.backIcon);
            _mainRv = view.FindViewById<RecyclerView>(Resource.Id.mainRv);
            _circleIndicator = view.FindViewById<CircularProgressIndicator>(Resource.Id.circleIndicator);
            _editTextTextPersonName = view.FindViewById<EditText>(Resource.Id.editTextTextPersonName);

            return view;
        }

        public override void SetUI()
        {
            _stepPositionTv.Text = ViewModel.StepPosition;
            _pageQuestionTv.Text = ViewModel.Title;
            _pageDescriptionTv.Text = ViewModel.Description;

            _circleIndicator.Progress = 50;

            SetRecyclerView();
            UpdateButton();
        }

        private void SetRecyclerView()
        {
            _mainRv.SetLayoutManager(new GridLayoutManager(Context, 3));
            _playersAvataresAdapter = new PlayersAvataresAdapter(ViewModel.Avatares);
            _playersAvataresAdapter.SelectedAvatar = ViewModel.SelectedAvatar;
            _playersAvataresAdapter.SelectAvatarCommand = ViewModel.SelectPlayerAvatarCommand;
            _mainRv.SetAdapter(_playersAvataresAdapter);
        }

        private void UpdateButton()
        {
            _continueButton.Alpha = ViewModel.ContinueCommand.CanExecute(null) ? 1 : 0.3f;
            _continueButton.Enabled = ViewModel.ContinueCommand.CanExecute(null);
        }

        protected override void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.ViewModelPropertyChanged(sender, e);

            switch(e.PropertyName)
            {
                case nameof(ViewModel.SelectedAvatar):
                    _playersAvataresAdapter.SelectedAvatar = ViewModel.SelectedAvatar;
                    _playersAvataresAdapter.NotifyDataSetChanged();
                    UpdateButton();
                    break;
                case nameof(ViewModel.PlayerName):
                    UpdateButton();
                    break;
            }
        }

        public override void SetupBindings()
        {
            _editTextTextPersonName.TextChanged += EditTextTextPersonNameTextChanged;
            _backIcon.Click += BackIconClick;
            ViewModel.CreatePlayerPage += CreatePlayerPage;
            ViewModel.ChooseQueenValuePage += ChooseQueenValuePage;
            _continueButton.Click += ContinueButtonClick;
        }

        public override void CleanBindings()
        {
            _editTextTextPersonName.TextChanged -= EditTextTextPersonNameTextChanged;
            _backIcon.Click -= BackIconClick;
            ViewModel.CreatePlayerPage -= CreatePlayerPage;
            ViewModel.ChooseQueenValuePage -= ChooseQueenValuePage;
            _continueButton.Click -= ContinueButtonClick;
        }
        private void ContinueButtonClick(object sender, EventArgs e)
        {
            ViewModel.ContinueCommand.Execute(this);
        }

        private void CreatePlayerPage(object sender, EventArgs e)
        {
            FragmentHelper.ShowFragment(this, new CreatePlayerFragment());
        }

        private void ChooseQueenValuePage(object sender, EventArgs e)
        {
            FragmentHelper.ShowFragment(this, new SelectSpadesQueenValueFragment());
        }

        private void BackIconClick(object sender, EventArgs e)
        {
            MainActivity.SupportFragmentManager.PopBackStack();
        }

        private void EditTextTextPersonNameTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            ViewModel.DefinePlayerNameCommand.Execute(e.Text.ToString());
        }
    }
}