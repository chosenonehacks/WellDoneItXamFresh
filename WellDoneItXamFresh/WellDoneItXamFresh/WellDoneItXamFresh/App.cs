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


            var mainPage = new ThemedTabbedPage();

            mainPage.AddTab<InboxPageModel>("Inbox", "inbox.png");
            mainPage.AddTab<TodayPageModel>("Today", "today.png");
            mainPage.AddTab<ContextListPageModel>("Contexts", "context.png");
            

            MainPage = mainPage;

            //var masterDetailNav = new ThemedMasterDetailNavigationContainer(); // FreshMasterDetailNavigationContainer();
            //masterDetailNav.Init("Menu", "menu.png");
            //masterDetailNav.AddPageWithIcon<InboxPageModel>("Inbox", "inbox.png");
            //masterDetailNav.AddPageWithIcon<TodayPageModel>("Today", "today.png");
            //masterDetailNav.AddPageWithIcon<ContextListPageModel>("Contexts", "context.png");
            //masterDetailNav.AddPageWithIcon<SettingsPageModel>("Settings", "settings.png");
            //MainPage = masterDetailNav;

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
