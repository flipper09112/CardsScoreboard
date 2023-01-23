using Android.OS;
using Android.Views;
using AndroidX.Fragment.App;
using AndroidX.Lifecycle;
using Autofac;
using CardsScoreboard.Helpers;
using CardsScoreboard.ViewModel;
using Prism.Mvvm;
using System;

namespace CardsScoreboard.UI.Fragments.Base
{
    public abstract class BaseFragment<TViewModel> : Fragment where TViewModel : BaseViewModel
    {
        protected TViewModel ViewModel { get; } = RegistIOCComponentsHelper.Container.Resolve<TViewModel>();

        protected abstract MainActivity MainActivity { get; }

        protected BaseFragment()
        {
        }

        public abstract override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState);

        public abstract void SetUI();

        public abstract void SetupBindings();
        public abstract void CleanBindings();

        public override void OnResume()
        {
            base.OnResume();

            ViewModel.PropertyChanged += ViewModelPropertyChanged;
            ViewModel?.Appearing();
            SetUI();
            SetupBindings();
        }

        public override void OnPause()
        {
            base.OnPause();

            ViewModel.PropertyChanged -= ViewModelPropertyChanged;
            ViewModel?.DisAppearing();
            CleanBindings();
        }

        protected virtual void ViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(ViewModel.IsBusy):
                    MainActivity.IsLoading(ViewModel.IsBusy);
                    return;
            }
        }
    }
}