using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using FreshMvvm;
using UIKit;
using WellDoneItXamFresh.Pages;
using Xamarin.Forms;

namespace WellDoneItXamFresh.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
        {
            if (Xamarin.Forms.Application.Current == null || Xamarin.Forms.Application.Current.MainPage == null)
            {
                return UIInterfaceOrientationMask.Portrait;
            }

            var mainPage = Xamarin.Forms.Application.Current.MainPage;

            if (mainPage is LoginPage ||
               (mainPage is FreshTabbedNavigationContainer && ((FreshTabbedNavigationContainer)mainPage).CurrentPage is LoginPage) ||
               (mainPage.Navigation != null && mainPage.Navigation.ModalStack.LastOrDefault() is LoginPage))
            {
                return UIInterfaceOrientationMask.Portrait;
            }

            return UIInterfaceOrientationMask.Portrait;
        }
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            SQLitePCL.CurrentPlatform.Init();

            LoadApplication(new App());
            
            return base.FinishedLaunching(app, options);
        }
    }
}
