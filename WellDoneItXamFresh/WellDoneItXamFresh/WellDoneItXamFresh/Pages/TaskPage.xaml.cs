using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using WellDoneIt.Model;
using WellDoneItXamFresh.PageModels;
using Xamarin.Forms;

namespace WellDoneItXamFresh.Pages
{
    public partial class TaskPage : ContentPage
    {
        public TaskPage()
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.Android)
            {
                ToolbarItems.Clear();

                fabsav.Clicked = AndroidAddButtonClicked;
                fabdel.Clicked = DeleteAddButtonClicked;
            }
        }

        void AndroidAddButtonClicked(object sender, EventArgs e)
        {
            var taskPageModel = this.BindingContext as TaskPageModel;
            

            taskPageModel?.SaveTaskSaveCommand.Execute(taskPageModel.WellDoneItTask);

            //taskPageModel?.CoreMethods.PopPageModel();
        }

        void DeleteAddButtonClicked(object sender, EventArgs e)
        {
            var taskPageModel = this.BindingContext as TaskPageModel;
            

            taskPageModel?.DeleteTaskCommand.Execute(taskPageModel.WellDoneItTask);

            //taskPageModel?.CoreMethods.PopPageModel();
        }
    }
}
