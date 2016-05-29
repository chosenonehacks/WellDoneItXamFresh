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
    public class ContextListPageModel : FreshBasePageModel
    {
        private readonly IWellDoneItMobileService _wellDoneItMobileService;

        public List<WellDoneItContext> WellDoneItContextList { get; set; } = new List<WellDoneItContext>();

        public ContextListPageModel(IWellDoneItMobileService wellDoneItMobileService)
        {
            if (wellDoneItMobileService == null) throw new ArgumentNullException("wellDoneItMobileService");
            _wellDoneItMobileService = wellDoneItMobileService;
        }

        private WellDoneItContext _selectedContext;
        public WellDoneItContext SelectedContext
        {
            get
            {
                return null;
            }
            set
            {
                _selectedContext = value;
                if (value != null)
                    ContextSelected.Execute(value);
            }
        }



        public Command<WellDoneItContext> ContextSelected
        {
            get
            {
                return new Command<WellDoneItContext>(async (context) => {
                    await CoreMethods.PushPageModel<ContextTaskPageModel>(context);
                });
            }
        }

        Command _loadContextsCommand;
        public Command LoadContextsCommand =>
            _loadContextsCommand ?? (_loadContextsCommand = new Command( ReloadContexts));

        public Command AddNewContextCommand
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<ContextPageModel>();
                });
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

        #region Loading contexts from Azure - temporay abondoned idea
        //public bool IsBusy { get; set; }

        //private async Task ReloadContexts()
        //{
        //    if (IsBusy)
        //        return;

        //    try
        //    {

        //        if (!Settings.IsLoggedIn)
        //        {
        //            await CoreMethods.PushPageModel<LoginPageModel>(null, false);

        //            return;
        //        }

        //        await _wellDoneItMobileService.Initialize();

        //        IsBusy = true;
        //        var contexts = await _wellDoneItMobileService.GetWellDoneItContexts();

        //        //First time logining - seeds initial contextes
        //        if (!contexts.Any())
        //        {
        //            await InsertInitContexts();
        //            contexts = await _wellDoneItMobileService.GetWellDoneItContexts();
        //        }

        //        WellDoneItContextList.ReplaceRange(contexts);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("OH NO!" + ex);
        //        await CoreMethods.DisplayAlert("Alert", "Unable to sync tasks, you may be offline", "ok");
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}

        //private async Task InsertInitContexts()
        //{
        //    await _wellDoneItMobileService.InsertInitContexts();
        //}
        #endregion

        public override void Init(object initData)
        {
            
        }

        

        protected override async void ViewIsAppearing(object sender, System.EventArgs e)
        {
			if (!Settings.IsLoggedIn)
			{
				await CoreMethods.PushPageModel<LoginPageModel>(null, false);

				return;
			}

            ReloadContexts();

            base.ViewIsAppearing(sender, e);
        }

        private void ReloadContexts()
        {
            WellDoneItContextList = new List<WellDoneItContext>
            {
                new WellDoneItContext
                {
                    Name = "@Home"
                },
                new WellDoneItContext
                {
                    Name = "@Work"
                },
                new WellDoneItContext
                {
                    Name = "@Shoping"
                },
                new WellDoneItContext
                {
                    Name = "@Someday"
                },
            };
        }

        protected override void ViewIsDisappearing(object sender, System.EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
        }

        
        public override void ReverseInit(object value)
        {
            var newContext = value as WellDoneItContext;
            if (!WellDoneItContextList.Contains(newContext))
            {
                WellDoneItContextList.Add(newContext);
            }
        }
    }
}
