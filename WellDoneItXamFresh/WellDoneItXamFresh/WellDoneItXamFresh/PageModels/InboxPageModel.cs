using FreshMvvm;
using MvvmHelpers;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellDoneIt.Model;
using WellDoneIt.Services;
using Xamarin.Forms;

namespace WellDoneItXamFresh.PageModels
{
    [ImplementPropertyChanged]
    class InboxPageModel : FreshBasePageModel
    {
        private readonly IWellDoneItMobileService _wellDoneItMobileService;

        public ObservableRangeCollection<WellDoneItTask> WellDoneItList { get; set; } = new ObservableRangeCollection<WellDoneItTask>();

        public InboxPageModel(IWellDoneItMobileService wellDoneItMobileService)
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

        

        private async Task ReloadTasks()
        {
            //TODO: Add busy indicator ON
            var tasks = await _wellDoneItMobileService.GetWellDoneItTasks();
            WellDoneItList.ReplaceRange(tasks.Where(t => t.DateUtc != DateTime.Today));
            //TODO: Add busy indicator OFF
        }

        


        public async override void Init(object initData)
        {
            //await ReloadTasks();
            
        }

        // Methods are automatically wired up to page
        protected async override void ViewIsAppearing(object sender, System.EventArgs e)
        {
            //CoreMethods.DisplayAlert ("Page is appearing", "", "Ok");
            await ReloadTasks();

            base.ViewIsAppearing(sender, e);
        }

        protected override void ViewIsDisappearing(object sender, System.EventArgs e)
        {
            //CoreMethods.DisplayAlert("Page is disappearing", "", "Ok");
            

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
