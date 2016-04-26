using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using MvvmHelpers;
using PropertyChanged;
using WellDoneIt.Model;
using WellDoneIt.Services;
using WellDoneItXamFresh.Helpers;
using Xamarin.Forms;

namespace WellDoneItXamFresh.PageModels
{
    [ImplementPropertyChanged]
    public class ContextTaskPageModel : FreshBasePageModel
    {
        private readonly IWellDoneItMobileService _wellDoneItMobileService;

        public ObservableRangeCollection<WellDoneItTask> WellDoneItList { get; set; } = new ObservableRangeCollection<WellDoneItTask>();

        public ContextTaskPageModel(IWellDoneItMobileService wellDoneItMobileService)
        {
            if (wellDoneItMobileService == null) throw new ArgumentNullException("wellDoneItMobileService");
            _wellDoneItMobileService = wellDoneItMobileService;
        }

        public WellDoneItContext SelectedWellDoneItContext { get; set; }

        public override void Init(object initData)
        {
            if (initData != null)
            {
                SelectedWellDoneItContext = initData as WellDoneItContext;
                PageTitle = "Context: " + SelectedWellDoneItContext.Name;
            }
        }

        public Command AddNewTaskCommand
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<TaskPageModel>();
                });
            }
        }

        public string PageTitle { get; set; }

        public bool IsBusy { get; set; }

        private async Task ReloadTasks(WellDoneItContext wellDoneItContext)
        {
            if (IsBusy)
                return;

            try
            {

                IsBusy = true;
                var tasks = await _wellDoneItMobileService.GetWellDoneItTasks();
                WellDoneItList.ReplaceRange(tasks.Where(t => t.Context == wellDoneItContext.Name));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("OH NO!" + ex);
                await CoreMethods.DisplayAlert("Alert", "Unable to get tasks for given context, you may be offline", "ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        WellDoneItTask _selectedTask;

        public WellDoneItTask SelectedTask
        {
            get
            {
                return null;
            }
            set
            {
                _selectedTask = value;
                if (value != null)
                    TaskSelected.Execute(value);
            }
        }

        public Command<WellDoneItTask> TaskSelected
        {
            get
            {
                return new Command<WellDoneItTask>(async (task) => {
                    await CoreMethods.PushPageModel<TaskPageModel>(task);
                });
            }
        }

        Command loadTasksCommand;
        public Command LoadTasksCommand =>
            loadTasksCommand ?? (loadTasksCommand = new Command(async () => await ReloadTasks(SelectedWellDoneItContext)));

        protected override async void ViewIsAppearing(object sender, System.EventArgs e)
        {
            await ReloadTasks(SelectedWellDoneItContext);

            base.ViewIsAppearing(sender, e);
        }

        protected override void ViewIsDisappearing(object sender, System.EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
        }


        public override void ReverseInit(object value)
        {
            
        }
    }
}
