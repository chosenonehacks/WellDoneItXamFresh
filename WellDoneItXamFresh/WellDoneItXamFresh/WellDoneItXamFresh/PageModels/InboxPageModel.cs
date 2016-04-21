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
using WellDoneItXamFresh.Helpers;
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

        public bool IsBusy { get; set; }

        private async Task ReloadTasks()
        {
            if (IsBusy)
                return;

            try
            {
                if (!Settings.IsLoggedIn)
                {
                    await _wellDoneItMobileService.Initialize();
                    var user = await DependencyService.Get<IAuthentication>().LoginAsync(_wellDoneItMobileService.MobileService, Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.Facebook);
                    if (user == null)
                        return;
                }

                IsBusy = true;
                var tasks = await _wellDoneItMobileService.GetWellDoneItTasks();
                WellDoneItList.ReplaceRange(tasks.Where(t => t.DateUtc != DateTime.Today));
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
