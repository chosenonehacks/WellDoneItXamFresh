using FreshMvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using WellDoneIt.Model;
using WellDoneIt.Services;
using WellDoneItXamFresh.Helpers;
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
        }

        //public List<WellDoneItContext> WellDoneItContextList { get; set; } = new List<WellDoneItContext>();
        public List<WellDoneItContext> WellDoneItContextList { get; set; }
        
        public WellDoneItContext SelectedContext { get; set; }


        public WellDoneItTask WellDoneItTask { get; set; }
        
        public override async void Init(object initData)
        {
            ReloadContexts();

            if (initData != null)
            {
                WellDoneItTask = initData as WellDoneItTask;
                SelectedContext = WellDoneItContextList.FirstOrDefault(c => c.Name == WellDoneItTask.Context);
                
            }
            else
            {
                WellDoneItTask = new WellDoneItTask();
                WellDoneItTask.Title = "Name your task";
                WellDoneItTask.DateUtc = DateTime.Today;
                //WellDoneItTask.Context = WellDoneItContextList.FirstOrDefault().Name;
                //SelectedContext.Name = WellDoneItTask.Context;

                isNewTask = true;
            }
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

        #region Contexts loaded from Azure - currently droped idea
        //private async wellDoneItTask<IEnumerable<WellDoneItContext>> ReloadContexts()
        //{
        //    if (IsBusy)
        //        return null;

        //    try
        //    {

        //        if (!Settings.IsLoggedIn)
        //        {
        //            await CoreMethods.PushPageModel<LoginPageModel>(null, false);

        //            return null;
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

        //        return contexts;
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
        //    return null;
        //}


        //private async Task InsertInitContexts()
        //{
        //    await _wellDoneItMobileService.InsertInitContexts();
        //}

        #endregion

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

        public bool IsBusy { get; set; }

        private async Task TaskOperationAsync(WellDoneItTask wellDoneItTask)
        {
            if (IsBusy)
                return;
            try
            {

                IsBusy = true;
                if(SelectedContext != null)
                wellDoneItTask.Context = SelectedContext.Name;

                if (isNewTask)
                {

                    await _wellDoneItMobileService.AddWellDoneItTask(wellDoneItTask);

                }
                else
                {
                    await _wellDoneItMobileService.UpdateWellDoneItTask(wellDoneItTask);

                }

                await CoreMethods.PopPageModel(wellDoneItTask);
            }
            catch(Exception ex)
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
        protected override async void ViewIsAppearing(object sender, System.EventArgs e)
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
