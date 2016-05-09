using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using WellDoneItXamFresh.PageModels;
using Xamarin.Forms;

namespace WellDoneItXamFresh.Pages
{
    public partial class ContextTaskPage : ContentPage
    {
        public ContextTaskPage()
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.Android)
            {
                ToolbarItems.Clear();

                fab.Clicked = AndroidAddButtonClicked;
            }
        }

        void AndroidAddButtonClicked(object sender, EventArgs e)
        {
            var page = FreshPageModelResolver.ResolvePageModel<TaskPageModel>();

            Navigation.PushAsync(page);
        }
    }
}
