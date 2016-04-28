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


            //var mainPage = new FreshTabbedNavigationContainer();

            //mainPage.AddTab<InboxPageModel>("Inbox", "plus.png");
            //mainPage.AddTab<TodayPageModel>("Today", "plus.png");
            //mainPage.AddTab<ContextListPageModel>("Contexts", null);
            //mainPage.AddTab<SettingsPageModel>("Settings", null);
            //MainPage = mainPage;

            var masterDetailNav = new ThemedMasterDetailNavigationContainer(); // FreshMasterDetailNavigationContainer();
            masterDetailNav.Init("Menu", "Menu.png");
            masterDetailNav.AddPageWithIcon<InboxPageModel>("Inbox", "check.png");
            masterDetailNav.AddPageWithIcon<TodayPageModel>("Today", "check.png");
            masterDetailNav.AddPageWithIcon<ContextListPageModel>("Contexts", "check.png");
            masterDetailNav.AddPageWithIcon<SettingsPageModel>("Settings", null);
            MainPage = masterDetailNav;

            //var tabbedNavigation = new FreshTabbedNavigationContainer();
            //tabbedNavigation.AddTab<ContactListPageModel>("Contacts", "contacts.png", null);
            //tabbedNavigation.AddTab<QuoteListPageModel>("Quotes", "document.png", null);
            //MainPage = tabbedNavigation;
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
