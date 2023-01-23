using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.Helpers;
using CardsScoreboard.UI.Adapters.Home;
using CardsScoreboard.UI.Fragments.Base;
using CardsScoreboard.UI.Fragments.Matchs;
using CardsScoreboard.ViewModel;
using Com.Airbnb.Lottie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsScoreboard.UI.Fragments
{
    public class HomePageFragment : BaseFragment<HomePageViewModel>
    {
        private RecyclerView _homeRv;
        private LottieAnimationView _animationView;
        private HomePageAdapter _homeAdapter;

        protected override MainActivity MainActivity => (MainActivity)Activity;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Inflate the layout for this fragment
            View view = inflater.Inflate(Resource.Layout.HomePageFragment, container, false);

            _homeRv = view.FindViewById<RecyclerView>(Resource.Id.homeRv);
            _animationView = view.FindViewById<LottieAnimationView>(Resource.Id.animation_view);

            _homeRv.SetLayoutManager(new LinearLayoutManager(Context));

            return view;
        }

        public override void SetUI()
        {
            MainActivity.ShowNavBar();
            SetCollapsingView();
            SetHomeList();
        }

        public override void SetupBindings()
        {
            ViewModel.ShowHeartsMatchPage += ShowHeartsMatchPage;
        }

        public override void CleanBindings()
        {
            ViewModel.ShowHeartsMatchPage -= ShowHeartsMatchPage;
        }

        private void ShowHeartsMatchPage(object sender, EventArgs e)
        {
            FragmentHelper.ShowFragment(this, new HeartsMatchFragment());
        }

        private void SetCollapsingView()
        {
            List<string> lottieItems = new List<string>() { "ic_home_lottie_1.json", "ic_home_lottie_2.json", "ic_home_lottie_3.json" };

            Random rnd = new Random();
            int num = rnd.Next(0, 3);

            _animationView.SetAnimation(lottieItems[num]);
            _animationView.RepeatMode = ValueAnimator.Infinite;
            _animationView.PlayAnimation();
        }

        private void SetHomeList()
        {
            _homeAdapter = new HomePageAdapter(ViewModel.HomePageItems, ViewModel.CurrentMatch);
            _homeAdapter.OpenCurrentMatchCommmand = ViewModel.OpenCurrentMatchCommmand;
            _homeRv.SetAdapter(_homeAdapter);
        }
    }
}