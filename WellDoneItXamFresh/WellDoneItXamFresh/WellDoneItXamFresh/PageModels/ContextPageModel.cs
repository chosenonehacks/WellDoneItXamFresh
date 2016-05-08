using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using PropertyChanged;
using WellDoneIt.Model;
using WellDoneIt.Services;
using Xamarin.Forms;

namespace WellDoneItXamFresh.PageModels
{
    [ImplementPropertyChanged]
    public class ContextPageModel : FreshBasePageModel 
    {
        private readonly IWellDoneItMobileService _wellDoneItMobileService;
        private bool isNewContext { get; set; }

        public ContextPageModel(IWellDoneItMobileService wellDoneItMobileService)
        {
            if (wellDoneItMobileService == null) throw new ArgumentNullException("wellDoneItMobileService");
            _wellDoneItMobileService = wellDoneItMobileService;
        }

        public WellDoneItContext WellDoneItContext { get; set; }

        public override void Init(object initData)
        {
            if (initData != null)
            {
                WellDoneItContext = initData as WellDoneItContext;
            }
            else
            {
                WellDoneItContext = new WellDoneItContext();
                isNewContext = true;
            }
        }

        public Command SettingsCommand
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<SettingsPageModel>();
                });
            }
        }

        public Command SaveCommand
        {
            get
            {
                return new Command(async () => {
                    await ContextOperationAsync(WellDoneItContext);
                }
                );
            }
        }

        public bool IsBusy { get; set; }

        private async Task ContextOperationAsync(WellDoneItContext context)
        {
            if (IsBusy)
                return;
            try
            {
                
                IsBusy = true;

                if (isNewContext)
                {

                    await _wellDoneItMobileService.AddWellDoneItContext(context);
                }
                else
                {
                    await _wellDoneItMobileService.UpdateWellDoneItContext(context);
                }

                await CoreMethods.PopPageModel(context);
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

        // Methods are automatically wired up to page
        protected override void ViewIsAppearing(object sender, System.EventArgs e)
        {

            base.ViewIsAppearing(sender, e);
        }

        protected override void ViewIsDisappearing(object sender, System.EventArgs e)
        {
            isNewContext = false;
            base.ViewIsDisappearing(sender, e);
        }
    }
}
