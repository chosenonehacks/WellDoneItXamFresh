using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WellDoneIt.Services;
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

            //mainPage.AddTab<InboxPageModel>("Inbox", null);
            //mainPage.AddTab<TodayPageModel>("Today", null);
            //MainPage = mainPage;

            var masterDetailNav = new FreshMasterDetailNavigationContainer();
            masterDetailNav.Init("Menu", "Menu.png");
            masterDetailNav.AddPage<InboxPageModel>("Inbox", null);
            masterDetailNav.AddPage<TodayPageModel>("Today", null);
            masterDetailNav.AddPage<ContextListPageModel>("Contexts", null);
            masterDetailNav.AddPage<SettingsPageModel>("Settings", null);
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
