using FreshMvvm;
using MvvmHelpers;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellDoneIt.Model;
using WellDoneIt.Services;
using Xamarin.Forms;

namespace WellDoneItXamFresh.PageModels
{
    [ImplementPropertyChanged]
    class TodayPageModel : FreshBasePageModel
    {
        private readonly IWellDoneItMobileService _wellDoneItMobileService;

        public ObservableRangeCollection<WellDoneItTask> WellDoneItList { get; set; } = new ObservableRangeCollection<WellDoneItTask>();

        public TodayPageModel(IWellDoneItMobileService wellDoneItMobileService)
        {
            if (wellDoneItMobileService == null) throw new ArgumentNullException("wellDoneItMobileService");
            _wellDoneItMobileService = wellDoneItMobileService;
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

        public Command AddNewTaskCommand
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<TaskPageModel>();
                });
            }
        }

        public bool IsBusy { get; set; }

        private async Task ReloadTasks()
        {
            if (IsBusy)
                return;

            try
            {

                IsBusy = true;
                var tasks = await _wellDoneItMobileService.GetWellDoneItTasks();
                WellDoneItList.ReplaceRange(tasks.Where(d => d.DateUtc == DateTime.Today));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("OH NO!" + ex);
                await CoreMethods.DisplayAlert("Alert", "Unable to sync tasks, you may be offline", "ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public override void Init(object initData)
        {
            
        }

        // Methods are automatically wired up to page
        protected async override void ViewIsAppearing(object sender, System.EventArgs e)
        {
            await ReloadTasks();

            base.ViewIsAppearing(sender, e);
        }

        protected override void ViewIsDisappearing(object sender, System.EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
        }

        // This is called when a page id pop'd
        public override void ReverseInit(object value)
        {
            var newTask = value as WellDoneItTask;
            if (!WellDoneItList.Contains(newTask))
            {
                WellDoneItList.Add(newTask);
            }
        }
    }
}
