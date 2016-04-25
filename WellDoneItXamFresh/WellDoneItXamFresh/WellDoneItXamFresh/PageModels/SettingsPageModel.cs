using FreshMvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellDoneIt.Services;
using WellDoneItXamFresh.Helpers;
using Xamarin.Forms;

namespace WellDoneItXamFresh.PageModels
{
    [ImplementPropertyChanged]
    public class SettingsPageModel : FreshBasePageModel
    {
        private readonly IWellDoneItMobileService _wellDoneItMobileService;

        public SettingsPageModel(IWellDoneItMobileService wellDoneItMobileService)
        {
            if (wellDoneItMobileService == null) throw new ArgumentNullException("wellDoneItMobileService");
            _wellDoneItMobileService = wellDoneItMobileService;
        }

        public string MobileServiceLoggedUserName { get; set; }

        public Command LogoutCommand
        {
            get
            {
                return new Command(async (loginMethod) => {
                    await LogoutAsync();
                });
            }
        }

        private async Task LogoutAsync()
        {
            await DependencyService.Get<IAuthentication>().LogoutAsync(_wellDoneItMobileService.MobileService);

            Settings.UserId = String.Empty;

            await _wellDoneItMobileService.ReInitialize();

            await CoreMethods.PushPageModel<InboxPageModel>(false);
            
        }

        public override void Init(object initData)
        {            

        }

        protected async override void ViewIsAppearing(object sender, System.EventArgs e)
        {
            if (Settings.IsLoggedIn)
                MobileServiceLoggedUserName = Settings.UserId;

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
