using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WellDoneIt.Services;
using WellDoneItXamFresh.Helpers;
using WellDoneItXamFresh.PageModels;
using Xamarin.Forms;

namespace WellDoneItXamFresh
{
    public class App : Application
    {
        public App()
        {
            FreshIOC.Container.Register<IWellDoneItMobileService, WellDoneItMobileService>();


            if (Device.OS == TargetPlatform.iOS)
            {
                var mainPage = new FreshTabbedNavigationContainer();

                mainPage.AddTab<InboxPageModel>("Inbox", "inbox.png");
                mainPage.AddTab<TodayPageModel>("Today", "today.png");
                mainPage.AddTab<ContextListPageModel>("Contexts", "context.png");
                MainPage = mainPage;
            }
            else
            {
                var masterDetailNav = new ThemedMasterDetailNavigationContainer(); // FreshMasterDetailNavigationContainer();

                masterDetailNav.Init("Menu", "menu.png");
                masterDetailNav.AddPageWithIcon<InboxPageModel>("Inbox", "inbox.png");
                masterDetailNav.AddPageWithIcon<TodayPageModel>("Today", "today.png");
                masterDetailNav.AddPageWithIcon<ContextListPageModel>("Contexts", "context.png");
                masterDetailNav.AddPageWithIcon<SettingsPageModel>("Settings", "settings.png");
                MainPage = masterDetailNav;
            }

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
