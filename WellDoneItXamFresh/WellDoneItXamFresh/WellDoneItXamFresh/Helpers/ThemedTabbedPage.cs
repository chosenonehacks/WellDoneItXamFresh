using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using Xamarin.Forms;

namespace WellDoneItXamFresh.Helpers
{
    public class ThemedTabbedPage : FreshTabbedNavigationContainer
    {
        protected override Page CreateContainerPage(Page page)
        {
            var navigation = new NavigationPage(page) { BackgroundColor = Color.Gray, BarTextColor = Color.White, BarBackgroundColor = Color.FromHex("#009688") };

            return navigation;
        }

        protected override Page CreateDefault(object item)
        {
            var tab = item as Xamarin.Forms.TabbedPage;
            tab.BackgroundColor = Color.White;
            
            return base.CreateDefault(item);
        }
    }
}
