using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WellDoneIt.Services;
using Xamarin.Forms;

namespace WellDoneItXamFresh
{
    public class App : Application
    {
        public App()
        {
            FreshIOC.Container.Register<IWellDoneItMobileService, WellDoneItMobileService>();

            // The root page of your application
            var mainPage = new FreshTabbedNavigationContainer();

            mainPage.AddTab<PageModels.InboxPageModel>("Inbox", null);
            mainPage.AddTab<PageModels.TodayPageModel>("Today", null);
            MainPage = mainPage;
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
