using FreshMvvm;
using Microsoft.WindowsAzure.MobileServices;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellDoneIt.Services;
using Xamarin.Forms;

namespace WellDoneItXamFresh.PageModels
{
    [ImplementPropertyChanged]
    public class LoginPageModel : FreshBasePageModel
    {
        private readonly IWellDoneItMobileService _wellDoneItMobileService;
        //private MobileServiceUser user;

        public LoginPageModel(IWellDoneItMobileService wellDoneItMobileService)
        {
            if (wellDoneItMobileService == null) throw new ArgumentNullException("wellDoneItMobileService");
            _wellDoneItMobileService = wellDoneItMobileService;
        }

        public bool IsLogedInPanelVisible { get; set; }

        public string MobileServiceLoggedUserName { get; set; }
        

        public Command<string> LoginCommand
        {
            get
            {
                return new Command<string>(async (loginMethod) => {
                    await LoginAsync(loginMethod);
                });
            }
        }

        public Command CloseLoginCommand
        {
            get
            {
                return new Command(async () => {
                  await CoreMethods.PopPageModel(false);
                });
            }
        }

        private async Task LoginAsync(string loginMethod)
        {
            

             MobileServiceUser user = null;

            await _wellDoneItMobileService.Initialize();

            switch (loginMethod)
            {
                case "facebook":
                    user = await DependencyService.Get<IAuthentication>().LoginAsync(_wellDoneItMobileService.MobileService, Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.Facebook);
                    break;
                case "twitter":
                    user = await DependencyService.Get<IAuthentication>().LoginAsync(_wellDoneItMobileService.MobileService, Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.Twitter);
                    break;
                case "google":
                    user = await DependencyService.Get<IAuthentication>().LoginAsync(_wellDoneItMobileService.MobileService, Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.Google);
                    break;
                default:
                    break;
            }



            if (user != null)
            {
                IsLogedInPanelVisible = false;
                MobileServiceLoggedUserName = user.UserId;
            }
        }

        public override void Init(object initData)
        {
            

        }
        
        protected async override void ViewIsAppearing(object sender, System.EventArgs e)
        {
            IsLogedInPanelVisible = true;


            base.ViewIsAppearing(sender, e);
        }

        protected override void ViewIsDisappearing(object sender, System.EventArgs e)
        {
            IsLogedInPanelVisible = false;

            base.ViewIsDisappearing(sender, e);
        }
        
        public override void ReverseInit(object value)
        {

        }
    }
}
