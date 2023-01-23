using Android.App;
using Android.Content;
using Android.Media.TV;
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
using Java.Time.Temporal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CardsScoreboard.UI.Fragments.AddGame
{
    public class ChoosePlayersFragment : BaseFragment<ChoosePlayersViewModel>
    {
        private View _view;
        private TextView _stepPositionTv;
        private TextView _pageQuestionTv;
        private TextView _pageDescriptionTv;
        private ImageView _continueButton;
        private ImageView _backIcon;
        private CircularProgressIndicator _circleIndicator;
        private RecyclerView _playersRv;
        private CustomSlider _slider;
        private PlayersAdapter _playersAdapter;

        protected override MainActivity MainActivity => (MainActivity)Activity;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (_view != null) return _view;

            // Inflate the layout for this fragment
            View view = inflater.Inflate(Resource.Layout.ChoosePlayersFragment, container, false);

            _view = view;
            _stepPositionTv = view.FindViewById<TextView>(Resource.Id.stepPositionTv);
            _pageQuestionTv = view.FindViewById<TextView>(Resource.Id.pageQuestionTv);
            _pageDescriptionTv = view.FindViewById<TextView>(Resource.Id.pageDescriptionTv);
            _continueButton = view.FindViewById<ImageView>(Resource.Id.continueButton);
            _backIcon = view.FindViewById<ImageView>(Resource.Id.backIcon);
            _circleIndicator = view.FindViewById<CircularProgressIndicator>(Resource.Id.circleIndicator);
            _playersRv = view.FindViewById<RecyclerView>(Resource.Id.mainRv);
            _slider = view.FindViewById<CustomSlider>(Resource.Id.slider);

            return view;
        }

        public override void SetUI()
        {
            _stepPositionTv.Text = ViewModel.StepPosition;
            _pageQuestionTv.Text = ViewModel.Title;
            _pageDescriptionTv.Text = ViewModel.Description;

            _circleIndicator.Progress = 25;

            UpdateButton();
            SetRecyclerView();
        }

        private void SetRecyclerView()
        {
            _playersRv.SetLayoutManager(new LinearLayoutManager(Context));
            _playersAdapter = new PlayersAdapter(ViewModel.Players);
            _playersAdapter.PlayersSelected = ViewModel.PlayersSelected;
            _playersAdapter.SelectPlayer = ViewModel.SelectPlayerCommand;
            _playersRv.SetAdapter(_playersAdapter);
        }

        private void UpdateButton()
        {
            _continueButton.Alpha = ViewModel.ContinueCommand.CanExecute(null) ? 1 : 0.3f;
            _continueButton.Enabled = ViewModel.ContinueCommand.CanExecute(null);
        }

        public override void SetupBindings()
        {
            _backIcon.Click += BackIconClick;
            _continueButton.Click += ContinueButtonClick;
            _slider.ValueChange += ValueChange;
            ViewModel.CreatePlayerPage += CreatePlayerPage;
            ViewModel.ChooseQueenValuePage += ChooseQueenValuePage;
        }

        public override void CleanBindings()
        {
            _backIcon.Click -= BackIconClick;
            _continueButton.Click -= ContinueButtonClick;
            _slider.ValueChange -= ValueChange;
            ViewModel.CreatePlayerPage -= CreatePlayerPage;
            ViewModel.ChooseQueenValuePage -= ChooseQueenValuePage;
        }

        private void CreatePlayerPage(object sender, EventArgs e)
        {
            FragmentHelper.ShowFragment(this, new CreatePlayerFragment());
        }

        private void ChooseQueenValuePage(object sender, EventArgs e)
        {
            FragmentHelper.ShowFragment(this, new SelectSpadesQueenValueFragment());
        }

        private void ValueChange(object sender, EventArgs e)
        {
            ViewModel.SelectPlayerCountCommand.Execute(sender);
        }

        private void ContinueButtonClick(object sender, EventArgs e)
        {
            ViewModel.ContinueCommand.Execute(this);
        }

        private void BackIconClick(object sender, EventArgs e)
        {
            MainActivity.SupportFragmentManager.PopBackStack();
        }

        protected override void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.ViewModelPropertyChanged(sender, e);

            switch(e.PropertyName)
            {
                case nameof(ViewModel.PlayersCount):
                case nameof(ViewModel.PlayersSelected):
                    _playersAdapter.PlayersSelected = ViewModel.PlayersSelected;
                    _playersAdapter.NotifyDataSetChanged();
                    UpdateButton();
                    break;
            }
        }
    }
}