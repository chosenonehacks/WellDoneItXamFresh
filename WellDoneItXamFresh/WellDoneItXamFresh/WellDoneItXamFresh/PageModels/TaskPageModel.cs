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
        
        public bool isNewTask { get; set; }

        public TaskPageModel(IWellDoneItMobileService wellDoneItMobileService)
        {
            if (wellDoneItMobileService == null) throw new ArgumentNullException("wellDoneItMobileService");
            _wellDoneItMobileService = wellDoneItMobileService;
        }

        //public List<WellDoneItContext> WellDoneItContextList { get; set; } = new List<WellDoneItContext>();
        public List<WellDoneItContext> WellDoneItContextList { get; set; }

        private WellDoneItContext _selectedContext;
        public WellDoneItContext SelectedContext
        {
            get { return _selectedContext; }
            set { _selectedContext = value; }
        }

        private WellDoneItTask _wellDoneItTask;
        public WellDoneItTask WellDoneItTask
        {
            get { return _wellDoneItTask; }
            set { _wellDoneItTask = value; }
        }

        private string _pageTitle;
        public string PageTitle
        {
            get { return _pageTitle; }
            set { _pageTitle = value; }
        }

        public override void Init(object initData)
        {
            ReloadContexts();

            if (initData != null)
            {
                WellDoneItTask = initData as WellDoneItTask;
                SelectedContext = WellDoneItContextList.FirstOrDefault(c => c.Name == WellDoneItTask.Context);

                PageTitle = "Edit task";
            }
            else
            {
                WellDoneItTask = new WellDoneItTask();
                //WellDoneItTask.Title = "Name your task";
                WellDoneItTask.DateUtc = DateTime.Today;

                PageTitle = "Add new task";

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

        public Command SaveTaskSaveCommand
        {
            get
            {
                return new Command(async () => {
                    await SaveTaskAsync();
                }
                );
            }
        }

        public Command DeleteTaskCommand
        {
            get
            {
                return new Command(async () => {
                    await DeleteTaskAsync();
                }
                );
            }
        }

        private async Task DeleteTaskAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                await _wellDoneItMobileService.DeleteWellDoneItTask(WellDoneItTask);

                await CoreMethods.PopPageModel();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("OH NO!" + ex);
                await CoreMethods.DisplayAlert("Alert", "Unable to delete task", "ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public bool IsBusy { get; set; }

        private async Task SaveTaskAsync()
        {
            if (IsBusy)
                return;
            try
            {

                IsBusy = true;
                if(SelectedContext != null)
                WellDoneItTask.Context = SelectedContext.Name;

                if (WellDoneItTask.UserId == null)
                    WellDoneItTask.UserId = _wellDoneItMobileService.MobileService.CurrentUser.UserId;

                if (isNewTask)
                {

                    await _wellDoneItMobileService.AddWellDoneItTask(WellDoneItTask);

                }
                else
                {
                    await _wellDoneItMobileService.UpdateWellDoneItTask(WellDoneItTask);

                }

                await CoreMethods.PopPageModel(WellDoneItTask);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("OH NO!" + ex);
                await CoreMethods.DisplayAlert("Alert", "Unable to sync tasks", "ok");
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
            isNewTask = false;
            base.ViewIsDisappearing(sender, e);
        }

        // This is called when a page id pop'd
        public override void ReverseInit(object value)
        {

        }
        
    }
}
