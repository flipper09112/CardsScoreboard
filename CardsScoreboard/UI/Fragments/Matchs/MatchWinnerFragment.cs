using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.Models.General;
using CardsScoreboard.UI.Fragments.Base;
using CardsScoreboard.ViewModel.Matchs;
using Com.Airbnb.Lottie;
using Plugin.CurrentActivity;

namespace CardsScoreboard.UI.Fragments.Matchs
{
    public class MatchWinnerFragment : BaseFragment<MatchWinnerViewModel>
    {
        private ImageView _winnerPlayerIcon;
        private ImageView _playerIcon;
        private TextView _playerName;
        private Button _finishBt;
        private ImageView _playerLoserIcon;
        private TextView _playerLoserName;

        protected override MainActivity MainActivity => Activity as MainActivity;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Inflate the layout for this fragment
            View view = inflater.Inflate(Resource.Layout.MatchWinnerFragment, container, false);

            _winnerPlayerIcon = view.FindViewById<ImageView>(Resource.Id.winnerPlayerIcon);
            _playerIcon = view.FindViewById<ImageView>(Resource.Id.playerIcon);
            _playerName = view.FindViewById<TextView>(Resource.Id.playerName);
            _finishBt = view.FindViewById<Button>(Resource.Id.finishBt);
            _playerLoserIcon = view.FindViewById<ImageView>(Resource.Id.playerLoserIcon);
            _playerLoserName = view.FindViewById<TextView>(Resource.Id.playerLoserName);

            return view;
        }

        public override void SetUI()
        {
            _playerName.Text = ViewModel.Winner.Name;
            int imageResourceId = Resources.GetIdentifier(ViewModel.Winner.IconName, "drawable", MainActivity.PackageName);
            _playerIcon.SetImageResource(imageResourceId);

            _playerLoserName.Text = ViewModel.BiggerLoser.Name;
            imageResourceId = Resources.GetIdentifier(ViewModel.BiggerLoser.IconName, "drawable", MainActivity.PackageName);
            _playerLoserIcon.SetImageResource(imageResourceId);
        }

        public override void SetupBindings()
        {
            MainActivity.BackPressed += BackPressed;
            _finishBt.Click += FinishBtClick;
        }

        public override void CleanBindings()
        {
            MainActivity.BackPressed -= BackPressed;
            _finishBt.Click -= FinishBtClick;
        }

        private void FinishBtClick(object sender, EventArgs e)
        {
            MainActivity.SupportFragmentManager.PopBackStack(nameof(HomePageFragment), 0);
        }

        private void BackPressed(object sender, EventArgs e)
        {
            MainActivity.SupportFragmentManager.PopBackStack(nameof(HomePageFragment), 0);
        }
    }
}

