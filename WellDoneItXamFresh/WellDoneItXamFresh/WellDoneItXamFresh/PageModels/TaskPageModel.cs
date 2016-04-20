using FreshMvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellDoneIt.Model;
using WellDoneIt.Services;
//using WellDoneItXamFresh.Helpers;
using Xamarin.Forms;

namespace WellDoneItXamFresh.PageModels
{
    [ImplementPropertyChanged]
    class TaskPageModel : FreshBasePageModel
    {
        private readonly IWellDoneItMobileService _wellDoneItMobileService;
        private bool isNewTask { get; set; }

        public TaskPageModel(IWellDoneItMobileService wellDoneItMobileService)
        {
            if (wellDoneItMobileService == null) throw new ArgumentNullException("wellDoneItMobileService");
            _wellDoneItMobileService = wellDoneItMobileService;

            this.WhenAny(HandleContactChanged, o => o.WellDoneItTask);
        }

        public WellDoneItTask WellDoneItTask { get; set; }

        void HandleContactChanged(string propertyName)
        {
            //handle the property changed, nice
        }


        public override void Init(object initData)
        {
            if (initData != null)
            {
                WellDoneItTask = initData as WellDoneItTask;
            }
            else
            {
                WellDoneItTask = new WellDoneItTask();
                WellDoneItTask.DateUtc = DateTime.Today;

                isNewTask = true;
            }
        }

        public Command SaveCommand
        {
            get
            {
                return new Command(async () => {
                    await TaskOperationAsync(WellDoneItTask);
                }
                );
            }
        }

        private async Task TaskOperationAsync(WellDoneItTask Task)
        {
            if (!WellDoneItXamFresh.Helpers.Settings.IsLoggedIn)
            {
                await _wellDoneItMobileService.Initialize();
                var user = await DependencyService.Get<IAuthentication>().LoginAsync(_wellDoneItMobileService.MobileService, Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.Facebook);
                if (user == null)
                    return;
            }

                if (isNewTask)
                {

                    await _wellDoneItMobileService.AddWellDoneItTask(Task);

                }
                else
                {
                    await _wellDoneItMobileService.UpdateWellDoneItTask(Task);

                }

                await CoreMethods.PopPageModel(Task);
            
        }

        // Methods are automatically wired up to page
        protected override void ViewIsAppearing(object sender, System.EventArgs e)
        {
            
            base.ViewIsAppearing(sender, e);
        }

        protected override void ViewIsDisappearing(object sender, System.EventArgs e)
        {
            isNewTask = false;
            base.ViewIsDisappearing(sender, e);
        }

        // This is called when a page id pop'd
        public override void ReverseInit(object value)
        {

        }
    }
}
