using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using WellDoneIt.Model;
using WellDoneItXamFresh.Services;
using WellDoneItXamFresh.Helpers;

namespace WellDoneIt.Services
{
    public class WellDoneItMobileService : IWellDoneItMobileService
    {  
        
        
        private IMobileServiceSyncTable<WellDoneItTask> _wellDoneItTaskSyncTable;
        private IMobileServiceSyncTable<WellDoneItContext> _wellDoneItContextSyncTable;

        public MobileServiceClient MobileService { get; set; }

        bool _isInitialized;

        public async Task Initialize()
        {
            if (_isInitialized)
                return;

            var handler = new AuthHandler();

            //Create our client
            MobileService = new MobileServiceClient("https://welldoneitmobileapp.azurewebsites.net", handler); 
            handler.Client = MobileService;

            if (!string.IsNullOrWhiteSpace(Settings.AuthToken) && !string.IsNullOrWhiteSpace(Settings.UserId))
            {
                MobileService.CurrentUser = new MobileServiceUser(Settings.UserId);
                MobileService.CurrentUser.MobileServiceAuthenticationToken = Settings.AuthToken;
            }

            const string path = "localsyncstore4.db";

            //setup our local sqlite store and intialize our table
            var store = new MobileServiceSQLiteStore(path);

            store.DefineTable<WellDoneItTask>();
            store.DefineTable<WellDoneItContext>();

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            //Get our sync table that will call out to azure
            _wellDoneItTaskSyncTable = MobileService.GetSyncTable<WellDoneItTask>();
            _wellDoneItContextSyncTable = MobileService.GetSyncTable<WellDoneItContext>();

            _isInitialized = true;
        }

        public async Task ReInitialize()
        {
            if (!_isInitialized)
                return;

            var token = new System.Threading.CancellationToken();

            try
            {
                await _wellDoneItTaskSyncTable.PurgeAsync("allTasks", _wellDoneItTaskSyncTable.CreateQuery(), token);
                await _wellDoneItContextSyncTable.PurgeAsync("allContexts", _wellDoneItContextSyncTable.CreateQuery(), token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem with purgeing tables" + ex);
            }
            

            _isInitialized = false;

            await this.Initialize();
        }



        public async Task<IEnumerable<WellDoneItTask>> GetWellDoneItTasks()
        {
            await Initialize();
            await SyncTasks();
            
            //return all tasks that are not completed
            return await _wellDoneItTaskSyncTable.Where(t => !t.Complete).OrderBy(c => c.DateUtc).ToEnumerableAsync();
        }

        public async Task<IEnumerable<WellDoneItContext>> GetWellDoneItContexts()
        {
            await Initialize();
            await SyncContext();

            //return all context that are not completed
            return await _wellDoneItContextSyncTable.ToEnumerableAsync();
        }

        public async Task AddWellDoneItTask(WellDoneItTask task)
        {
            await Initialize();                       

            await _wellDoneItTaskSyncTable.InsertAsync(task);

            await SyncTasks();
        }

        //Used only once to populate standard contexts
        public async Task InsertInitContexts()
        {
            var homeContext = new WellDoneItContext() { Name = "@Home", UserId = this.MobileService.CurrentUser.UserId};

            var workContext = new WellDoneItContext() { Name = "@Work", UserId = this.MobileService.CurrentUser.UserId};

            try
            {
                await this.AddWellDoneItContext(homeContext);
                await this.AddWellDoneItContext(workContext);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to insert initial contexts" + ex);
            }
        }

        public async Task AddWellDoneItContext(WellDoneItContext context)
        {
            await Initialize();

            await _wellDoneItContextSyncTable.InsertAsync(context);

            await SyncContext();
        }

        public async Task UpdateWellDoneItTask(WellDoneItTask task)
        {
            await _wellDoneItTaskSyncTable.UpdateAsync(task);

            await SyncTasks();
        }

        public async Task UpdateWellDoneItContext(WellDoneItContext context)
        {
            await _wellDoneItContextSyncTable.UpdateAsync(context);

            await SyncContext();
        }

        public async Task SyncTasks()
        {
            try
            {
                //pull down all latest changes and then push current tasks up
                await _wellDoneItTaskSyncTable.PullAsync("allTasks", _wellDoneItTaskSyncTable.CreateQuery());
                
                await MobileService.SyncContext.PushAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync tasks, that is alright as we have offline capabilities: " + ex);
            }
        }

        public async Task SyncContext()
        {
            try
            {
                await _wellDoneItContextSyncTable.PullAsync("allContexts", _wellDoneItTaskSyncTable.CreateQuery());

                await MobileService.SyncContext.PushAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync context, that is alright as we have offline capabilities: " + ex);
            }
        }
    }
}