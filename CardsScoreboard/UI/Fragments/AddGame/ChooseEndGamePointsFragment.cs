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
using CardsScoreboard.UI.LayoutManagers;
using CardsScoreboard.ViewModel.AddGame;
using Google.Android.Material.ProgressIndicator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using static CardsScoreboard.UI.LayoutManagers.PickerLayoutManager;

namespace CardsScoreboard.UI.Fragments.AddGame
{
    public class ChooseEndGamePointsFragment : BaseFragment<ChooseEndGamePointsViewModel>, IOnItemPickerLayoutManagerListener
    {
        private TextView _stepPositionTv;
        private TextView _pageQuestionTv;
        private TextView _pageDescriptionTv;
        private ImageView _continueButton;
        private ImageView _backIcon;
        private RecyclerView _mainRv;
        private CircularProgressIndicator _circleIndicator;
        private ValuesAdapter _valuesAdapter;

        protected override MainActivity MainActivity => (MainActivity)Activity;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Inflate the layout for this fragment
            View view = inflater.Inflate(Resource.Layout.ChooseEndGamePointsFragment, container, false);

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
            _stepPositionTv.Text = ViewModel.StepPosition;
            _pageQuestionTv.Text = ViewModel.Title;
            _pageDescriptionTv.Text = ViewModel.Description;

            _circleIndicator.Progress = 90;

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
            _mainRv.SetLayoutManager(new PickerLayoutManager(Context, this));
            _valuesAdapter = new ValuesAdapter(ViewModel.MaxPointsValues);
            _valuesAdapter.ValueSelected = ViewModel.MaxPointsValue;
            _valuesAdapter.ItemClick -= ItemClick;
            _valuesAdapter.ItemClick += ItemClick;
            _mainRv.SetAdapter(_valuesAdapter);

            var padding = ScreenHelper.GetScreenWidth(Context) / 2 - ScreenHelper.DpToPx(Context, 40);
            _mainRv.SetPadding(padding, 0, padding, 0);

            _mainRv.ScrollToPosition(ViewModel.MaxPointsValues.IndexOf(ViewModel.MaxPointsValue));
        }

        public override void SetupBindings()
        {
            _backIcon.Click += BackIconClick;
            _continueButton.Click += ContinueButtonClick;
            ViewModel.ShowNewGameResumePage += ShowNewGameResumePage;
        }

        public override void CleanBindings()
        {
            _backIcon.Click -= BackIconClick;
            _continueButton.Click -= ContinueButtonClick;
            ViewModel.ShowNewGameResumePage -= ShowNewGameResumePage;
        }

        private void ShowNewGameResumePage(object sender, EventArgs e)
        {
            FragmentHelper.ShowFragment(this, new AddNewGameResumeFragment());
        }

        private void BackIconClick(object sender, EventArgs e)
        {
            MainActivity.SupportFragmentManager.PopBackStack();
        }

        private void ContinueButtonClick(object sender, EventArgs e)
        {
            ViewModel.ContinueCommand.Execute(this);
        }

        private void ItemClick(object sender, EventArgs e)
        {
            _mainRv.SmoothScrollToPosition(_mainRv.GetChildLayoutPosition((View)sender));
        }

        public void OnItemSelected(int layoutPosition)
        {
            ViewModel.SelecteMaxPointsValueCommand.Execute(layoutPosition);
        }

        protected override void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.ViewModelPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case nameof(ViewModel.MaxPointsValue):
                    _valuesAdapter.ValueSelected = ViewModel.MaxPointsValue;
                    _valuesAdapter.NotifyDataSetChanged();
                    break;
            }
        }
    }
}