using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WellDoneIt.Services;
using WellDoneItXamFresh.Helpers;
using WellDoneItXamFresh.PageModels;
using WellDoneItXamFresh.Pages;
using Xamarin.Forms;

namespace WellDoneItXamFresh
{
    public class App : Application
    {
        public App()
        {
            FreshIOC.Container.Register<IWellDoneItMobileService, WellDoneItMobileService>();


            if (Device.OS == TargetPlatform.Android)
            {
                MainPage = new SplashPage();
            }
            else
            {
                //var masterDetailNav = new ThemedMasterDetailNavigationContainer(); // FreshMasterDetailNavigationContainer();

                //masterDetailNav.Init("Menu", "menu.png");
                //masterDetailNav.AddPageWithIcon<InboxPageModel>("Inbox", "inboxm.png");
                //masterDetailNav.AddPageWithIcon<TodayPageModel>("Today", "todaym.png");
                //masterDetailNav.AddPageWithIcon<ContextListPageModel>("Contexts", "contextm.png");
                //masterDetailNav.AddPageWithIcon<SettingsPageModel>("Settings", "settingsm.png");
                //MainPage = masterDetailNav;
                var mainPage = new FreshTabbedNavigationContainer();

                mainPage.AddTab<InboxPageModel>("Inbox", "inboxm.png");
                mainPage.AddTab<TodayPageModel>("Today", "eventm.png");
                mainPage.AddTab<ContextListPageModel>("Contexts", "contextm.png");
                MainPage = mainPage;
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
