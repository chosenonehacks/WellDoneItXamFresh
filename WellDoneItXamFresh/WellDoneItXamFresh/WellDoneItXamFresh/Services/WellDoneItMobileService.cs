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

namespace WellDoneIt.Services
{
    public class WellDoneItMobileService : IWellDoneItMobileService
    {
        
        private MobileServiceCollection<WellDoneItTask, WellDoneItTask> _items;
        
        private IMobileServiceSyncTable<WellDoneItTask> _wellDoneItTaskSyncTable;

        public MobileServiceClient MobileService { get; set; }

        bool _isInitialized;

        public async Task Initialize()
        {
            if (_isInitialized)
                return;
            //Create our client
            MobileService = new MobileServiceClient("http://welldoneitmobileapp.azurewebsites.net");

            const string path = "syncstore.db";

            //setup our local sqlite store and intialize our table
            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<WellDoneItTask>();
            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            //Get our sync table that will call out to azure
            _wellDoneItTaskSyncTable = MobileService.GetSyncTable<WellDoneItTask>();

            //_wellDoneItTaskTable = MobileService.GetTable<WellDoneItTask>();

            _isInitialized = true;
        }

        
        public async Task<IEnumerable<WellDoneItTask>> GetWellDoneItTasks()
        {
            await Initialize();
            await SyncTasks();
            
            //return all tasks that are not completed
            return await _wellDoneItTaskSyncTable.Where(t => !t.Complete).OrderBy(c => c.DateUtc).ToEnumerableAsync();
        }

        public async Task AddWellDoneItTask(WellDoneItTask task)
        {
            await Initialize();                       

            await _wellDoneItTaskSyncTable.InsertAsync(task);

            await SyncTasks();
        }

        public async Task UpdateWellDoneItTask(WellDoneItTask task)
        {
            await _wellDoneItTaskSyncTable.UpdateAsync(task);

            await SyncTasks();
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
    }
}