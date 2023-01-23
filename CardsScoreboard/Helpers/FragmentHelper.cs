using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Fragment.App;
using CardsScoreboard.UI.Fragments;
using CardsScoreboard.UI.Fragments.AddGame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using static Android.Views.SurfaceControl;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace CardsScoreboard.Helpers
{
    public static class FragmentHelper
    {
        public static void ShowFragment(this AppCompatActivity activity, Fragment fragment)
        {
            // Inicie uma transação de fragmento
            var transaction = activity.SupportFragmentManager.BeginTransaction().SetCustomAnimations(Resource.Animation.slide_in,
                                                                                                    Resource.Animation.fade_out,
                                                                                                    Resource.Animation.fade_in,
                                                                                                    Resource.Animation.slide_out); ;

            // Adicione o fragmento ao layout da activity
            transaction.Replace(Resource.Id.fragment_container, fragment, fragment.GetType().Name);
            transaction.AddToBackStack(fragment.GetType().Name);

            // Aplique as alterações
            transaction.Commit();
        }

        internal static void ShowFragment(Fragment currentFragment, Fragment newFragment)
        {
            // Inicie uma transação de fragmento
            var transaction = currentFragment.Activity.SupportFragmentManager.BeginTransaction().SetCustomAnimations(Resource.Animation.slide_in, 
                                                                                                                     Resource.Animation.fade_out,
                                                                                                                     Resource.Animation.fade_in,
                                                                                                                     Resource.Animation.slide_out);

            // Adicione o fragmento ao layout da activity
            transaction.Replace(Resource.Id.fragment_container, newFragment, newFragment.GetType().Name);
            transaction.AddToBackStack(newFragment.GetType().Name);

            // Aplique as alterações
            transaction.Commit();
        }

        internal static void ShowNewFragment(AppCompatActivity activity, Fragment fragment)
        { 
            // Inicie uma transação de fragmento
            var transaction = activity.SupportFragmentManager.BeginTransaction().SetCustomAnimations(Resource.Animation.slide_in,
                                                                                                    Resource.Animation.fade_out,
                                                                                                    Resource.Animation.fade_in,
                                                                                                    Resource.Animation.slide_out);

            // Adicione o fragmento ao layout da activity
            transaction.Replace(Resource.Id.fragment_container, fragment, fragment.GetType().Name);
            transaction.AddToBackStack(fragment.GetType().Name);

            // Aplique as alterações
            transaction.Commit();
        }
    }
}