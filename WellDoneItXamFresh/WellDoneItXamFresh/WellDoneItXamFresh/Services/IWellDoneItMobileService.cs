using System.Collections.Generic;
using System.Threading.Tasks;
using WellDoneIt.Model;

namespace WellDoneIt.Services
{
    public interface IWellDoneItMobileService
    {
        Task Initialize();

        Task ReInitialize();

        Task<IEnumerable<WellDoneItTask>>GetWellDoneItTasks(); 

        Task<IEnumerable<WellDoneItContext>> GetWellDoneItContexts(); 

        Task AddWellDoneItTask(WellDoneItTask task);

        Task AddWellDoneItContext(WellDoneItContext context);

        Task InsertInitContexts();

        Task UpdateWellDoneItTask(WellDoneItTask task); 

        Task UpdateWellDoneItContext(WellDoneItContext task); 

        Task SyncTasks();

        Microsoft.WindowsAzure.MobileServices.MobileServiceClient MobileService { get; set; }
    }
}