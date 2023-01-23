using System;
using Android.OS;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.UI.Fragments.Base;
using CardsScoreboard.ViewModel.Matchs;
using Com.Airbnb.Lottie;

namespace CardsScoreboard.UI.Fragments.Matchs
{
    public class MatchWinnerFragment : BaseFragment<MatchWinnerViewModel>
    {
        protected override MainActivity MainActivity => Activity as MainActivity;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Inflate the layout for this fragment
            View view = inflater.Inflate(Resource.Layout.MatchWinnerFragment, container, false);

            return view;
        }

        public override void SetUI()
        {
        }

        public override void SetupBindings()
        {
        }

        public override void CleanBindings()
        {
        }
    }
}

