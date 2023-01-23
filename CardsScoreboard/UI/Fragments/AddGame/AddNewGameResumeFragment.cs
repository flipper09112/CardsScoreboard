using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.CardView.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.Helpers;
using CardsScoreboard.Models.General;
using CardsScoreboard.Models.Match;
using CardsScoreboard.UI.Fragments.Base;
using CardsScoreboard.UI.Fragments.Matchs;
using CardsScoreboard.ViewModel.AddGame;
using Google.Android.Material.ProgressIndicator;
using Plugin.CurrentActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsScoreboard.UI.Fragments.AddGame
{
    public class AddNewGameResumeFragment : BaseFragment<AddNewGameResumeViewModel>
    {
        private TextView _pageQuestionTv;
        private TextView _descriptionTv;
        private ImageView _backIcon;
        private CardView _gameCardView;
        private TextView _gameCardViewTv;
        private ImageView _gameCardViewIg;
        private TextView _gameNameCardViewTv;
        private CardView _pointsCardView;
        private TextView _pointsCardViewTv;
        private ImageView _pointsCardViewIg;
        private TextView _pointsValueCardViewTv;
        private CardView _otherCardView;
        private TextView _otherCardViewTv;
        private ImageView _otherCardViewIg;
        private TextView _otherValueCardViewTv;
        private CardView _playersCardView;
        private TextView _playersCardViewTv;
        private ImageView _playersCardViewIg;
        private TextView _playersValueCardViewTv;
        private Button _createGameButton;

        protected override MainActivity MainActivity => (MainActivity)Activity;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Inflate the layout for this fragment
            View view = inflater.Inflate(Resource.Layout.AddNewGameResumeFragment, container, false);

            _pageQuestionTv = view.FindViewById<TextView>(Resource.Id.pageQuestionTv);
            _descriptionTv = view.FindViewById<TextView>(Resource.Id.descriptionTv);
            _backIcon = view.FindViewById<ImageView>(Resource.Id.backIcon);

            _gameCardView = view.FindViewById<CardView>(Resource.Id.gameCardView);
            _gameCardViewTv = view.FindViewById<TextView>(Resource.Id.gameCardViewTv);
            _gameCardViewIg = view.FindViewById<ImageView>(Resource.Id.gameCardViewIg);
            _gameNameCardViewTv = view.FindViewById<TextView>(Resource.Id.gameNameCardViewTv);

            _pointsCardView = view.FindViewById<CardView>(Resource.Id.pointsCardView);
            _pointsCardViewTv = view.FindViewById<TextView>(Resource.Id.pointsCardViewTv);
            _pointsCardViewIg = view.FindViewById<ImageView>(Resource.Id.pointsCardViewIg);
            _pointsValueCardViewTv = view.FindViewById<TextView>(Resource.Id.pointsValueCardViewTv);

            _otherCardView = view.FindViewById<CardView>(Resource.Id.otherCardView);
            _otherCardViewTv = view.FindViewById<TextView>(Resource.Id.otherCardViewTv);
            _otherCardViewIg = view.FindViewById<ImageView>(Resource.Id.otherCardViewIg);
            _otherValueCardViewTv = view.FindViewById<TextView>(Resource.Id.otherValueCardViewTv);

            _playersCardView = view.FindViewById<CardView>(Resource.Id.playersCardView);
            _playersCardViewTv = view.FindViewById<TextView>(Resource.Id.playersCardViewTv);
            _playersCardViewIg = view.FindViewById<ImageView>(Resource.Id.playersCardViewIg);
            _playersValueCardViewTv = view.FindViewById<TextView>(Resource.Id.playersValueCardViewTv);

            _createGameButton = view.FindViewById<Button>(Resource.Id.createGameButton);

            return view;
        }

        public override void SetUI()
        {
            _pageQuestionTv.Text = ViewModel.PageTitle;
            _descriptionTv.Text = ViewModel.Description;

            _gameNameCardViewTv.Text = ViewModel.GameName;
            _playersValueCardViewTv.Text = ViewModel.PlayersNumber;
            _pointsValueCardViewTv.Text = ViewModel.GamePoints;
            _otherValueCardViewTv.Text = ViewModel.OtherValue;

            int imageResourceId = Activity.Resources.GetIdentifier(ViewModel.GameImage, "drawable", CrossCurrentActivity.Current.Activity.PackageName);
            _gameCardViewIg.SetImageResource(imageResourceId);

            imageResourceId = Activity.Resources.GetIdentifier(ViewModel.OtherCardImage, "drawable", CrossCurrentActivity.Current.Activity.PackageName);
            _otherCardViewIg.SetImageResource(imageResourceId);
        }

        public override void SetupBindings()
        {
            _backIcon.Click += BackIconClick;
            _createGameButton.Click += CreateGameButtonClick;
            ViewModel.ShowMatchPage += ShowMatchPage;
        }

        public override void CleanBindings()
        {
            _backIcon.Click -= BackIconClick;
            _createGameButton.Click -= CreateGameButtonClick;
            ViewModel.ShowMatchPage -= ShowMatchPage;
        }

        private void ShowMatchPage(object sender, EventArgs e)
        {
            MainActivity.SupportFragmentManager.PopBackStack(nameof(HomePageFragment), 0);
            FragmentHelper.ShowFragment(this, new HeartsMatchFragment());
        }

        private void CreateGameButtonClick(object sender, EventArgs e)
        {
            ViewModel.CreateGameCommand.Execute(null);
        }

        private void BackIconClick(object sender, EventArgs e)
        {
            MainActivity.SupportFragmentManager.PopBackStack();
        }
    }
}