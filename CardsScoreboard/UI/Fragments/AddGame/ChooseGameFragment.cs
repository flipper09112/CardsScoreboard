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
using CardsScoreboard.ViewModel.AddGame;
using Com.Airbnb.Lottie;
using Google.Android.Material.ProgressIndicator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CardsScoreboard.UI.Fragments.AddGame
{
    public class ChooseGameFragment : BaseFragment<ChooseGameViewModel>
    {
        private TextView _stepPositionTv;
        private TextView _pageQuestionTv;
        private TextView _pageDescriptionTv;
        private ImageView _continueButton;
        private ImageView _backIcon;
        private RecyclerView _mainRv;
        private CircularProgressIndicator _circleIndicator;
        private GamesAdapter _gamesAdapter;

        protected override MainActivity MainActivity => (MainActivity)Activity;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Inflate the layout for this fragment
            View view = inflater.Inflate(Resource.Layout.ChooseGameFragment, container, false);

            _stepPositionTv = view.FindViewById<TextView>(Resource.Id.stepPositionTv);
            _pageQuestionTv = view.FindViewById<TextView>(Resource.Id.pageQuestionTv);
            _pageDescriptionTv = view.FindViewById<TextView>(Resource.Id.pageDescriptionTv);
            _continueButton = view.FindViewById<ImageView>(Resource.Id.continueButton);
            _backIcon = view.FindViewById<ImageView>(Resource.Id.backIcon);
            _mainRv = view.FindViewById<RecyclerView>(Resource.Id.mainRv);
            _circleIndicator = view.FindViewById<CircularProgressIndicator>(Resource.Id.circleIndicator);

            return view;
        }

        public override void SetUI()
        {
            MainActivity.HideNavBar();

            _stepPositionTv.Text = ViewModel.StepPosition;
            _pageQuestionTv.Text = ViewModel.Title;
            _pageDescriptionTv.Text = ViewModel.Description;

            ConfigMainView();
            UpdateButton();
        }

        private void UpdateButton()
        {
            _continueButton.Alpha = ViewModel.ContinueCommand.CanExecute(null) ? 1 : 0.3f;
            _continueButton.Enabled = ViewModel.ContinueCommand.CanExecute(null);
        }

        private void ConfigMainView()
        {
            _mainRv.SetLayoutManager(new GridLayoutManager(Context, 2));
            _gamesAdapter = new GamesAdapter(ViewModel.Items, ViewModel.SelectGameCommand);
            _gamesAdapter.GameSelected = ViewModel.GameSelected;
            _mainRv.SetAdapter(_gamesAdapter);
        }

        public override void SetupBindings()
        {
            _backIcon.Click += BackIconClick;
            _continueButton.Click += ContinueButtonClick;
            ViewModel.PropertyChanged += ViewModelPropertyChanged;
            ViewModel.NextStepPage += NextStepPage;
            MainActivity.BackPressed += BackPressed;
        }

        public override void CleanBindings()
        {
            _backIcon.Click -= BackIconClick;
            _continueButton.Click -= ContinueButtonClick;
            ViewModel.PropertyChanged -= ViewModelPropertyChanged;
            MainActivity.BackPressed -= BackPressed;
        }

        private void BackPressed(object sender, EventArgs e)
        {
            BackIconClick(null, null);
        }

        private void NextStepPage(object sender, EventArgs e)
        {
            FragmentHelper.ShowFragment(this, new ChoosePlayersFragment());
        }

        protected override void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.ViewModelPropertyChanged(sender, e);

            switch(e.PropertyName)
            {
                case nameof(ViewModel.GameSelected):
                    _gamesAdapter.GameSelected = ViewModel.GameSelected;
                    _gamesAdapter.NotifyDataSetChanged();
                    break;
            }

            UpdateButton();
        }

        private void BackIconClick(object sender, EventArgs e)
        {
            MainActivity.SupportFragmentManager.PopBackStack();
        }

        private void ContinueButtonClick(object sender, EventArgs e)
        {
            ViewModel.ContinueCommand.Execute(this);
        }
    }
}