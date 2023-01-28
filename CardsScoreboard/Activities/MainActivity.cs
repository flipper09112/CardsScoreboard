using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Google.Android.Material.BottomNavigation;
using AndroidX.Fragment.App;
using Android.Widget;
using System.ComponentModel;
using CardsScoreboard.ViewModel;
using Autofac;
using CardsScoreboard.Helpers;
using CardsScoreboard.UI.Fragments;
using CardsScoreboard.UI.Fragments.AddGame;
using Google.Android.Material.BottomAppBar;
using Plugin.CurrentActivity;
using Android.Window;
using CardsScoreboard.UI.Fragments.Matchs;

namespace CardsScoreboard
{
    [Activity(Label = "@string/app_name", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        public static FrameLayout FragmentContainer;
        private ProgressBar _progressBar;
        private BottomAppBar _bottomAppBar;
        private BottomNavigationView _bottomNavigationView;
        private FloatingActionButton _fab;

        public EventHandler BackPressed;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            RegistIOCServices();

            FragmentContainer = FindViewById<FrameLayout>(Resource.Id.fragment_container);

            _progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);

            _bottomAppBar = FindViewById<BottomAppBar>(Resource.Id.bottomAppBar);

            _bottomNavigationView = FindViewById<BottomNavigationView>(Resource.Id.bottomNavigationView);
            _bottomNavigationView.Background = null;
            _bottomNavigationView.Menu.GetItem(2).SetEnabled(false);

            _fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            _fab.Click -= FabOnClick;
            _fab.Click += FabOnClick;

            ShowHomeFragment();
        }

        private void ShowHomeFragment()
        {
            FragmentHelper.ShowFragment(this, new HomePageFragment());
        }

        private void RegistIOCServices()
        {
            var builder = new ContainerBuilder();
            RegistIOCComponentsHelper.Regist(builder);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            FragmentHelper.ShowNewFragment(this, new ChooseGameFragment());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        internal void IsLoading(bool isBusy)
        {
            _progressBar.Visibility = isBusy ? ViewStates.Visible : ViewStates.Gone;
        }

        internal void HideNavBar()
        {
            _bottomAppBar.Visibility = ViewStates.Invisible;
            _fab.Visibility = ViewStates.Invisible;
        }

        internal void ShowNavBar()
        {
            _bottomAppBar.Visibility = ViewStates.Visible;
            _fab.Visibility = ViewStates.Visible;
        }

        public override void OnBackPressed()
        {
            var fragmentInstance = SupportFragmentManager.FindFragmentById(Resource.Id.fragment_container);
            
            if (fragmentInstance is MatchWinnerFragment)
            {
                BackPressed?.Invoke(null, null);
                return;
            }

            base.OnBackPressed();
        }
    }
}
