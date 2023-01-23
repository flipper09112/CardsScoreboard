using System;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Ioc;
using Prism.Navigation;

namespace CardsScoreboard.ViewModel
{
    public abstract class BaseViewModel : BindableBase
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        public abstract void Appearing();
        public abstract void DisAppearing();
    }
}