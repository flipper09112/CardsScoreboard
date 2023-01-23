using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Lifecycle;
using Autofac;
using CardsScoreboard.Services;
using CardsScoreboard.Services.Implementations;
using CardsScoreboard.Services.Interfaces;
using CardsScoreboard.ViewModel;
using CardsScoreboard.ViewModel.AddGame;
using CardsScoreboard.ViewModel.AddGame.Hearts;
using CardsScoreboard.ViewModel.Matchs;
using Kotlin.Reflect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsScoreboard.Helpers
{
    public static class RegistIOCComponentsHelper
    {
        public static IContainer Container { get; private set; }

        internal static void Regist(ContainerBuilder builder)
        {
            RegistViewModels(builder);
            RegistServices(builder);

            Container = builder.Build();
        }

        private static void RegistServices(ContainerBuilder builder)
        {
            builder.RegisterType<GamesManagerService>().As<IGamesManagerService>().SingleInstance();
            builder.RegisterType<MatchManagerService>().As<IMatchManagerService>().SingleInstance();
        }

        private static void RegistViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<MatchWinnerViewModel>().InstancePerDependency();
            builder.RegisterType<HomePageViewModel>().InstancePerDependency();
            builder.RegisterType<ChooseGameViewModel>().InstancePerDependency();
            builder.RegisterType<ChoosePlayersViewModel>().InstancePerDependency();
            builder.RegisterType<CreatePlayerViewModel>().InstancePerDependency();
            builder.RegisterType<SelectSpadesQueenValueViewModel>().InstancePerDependency();
            builder.RegisterType<ChooseEndGamePointsViewModel>().InstancePerDependency();
            builder.RegisterType<AddNewGameResumeViewModel>().InstancePerDependency();
            builder.RegisterType<HeartsMatchViewModel>().InstancePerDependency();
        }
    }
}