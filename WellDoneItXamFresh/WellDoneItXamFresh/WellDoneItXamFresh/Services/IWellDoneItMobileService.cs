using System.Collections.Generic;
using System.Threading.Tasks;
using WellDoneIt.Model;

namespace WellDoneIt.Services
{
    public interface IWellDoneItMobileService
    {
        Task Initialize();

        Task<IEnumerable<WellDoneItTask>>GetWellDoneItTasks();

        Task AddWellDoneItTask(WellDoneItTask task);

        Task UpdateWellDoneItTask(WellDoneItTask task);

        Task SyncTasks();

        Microsoft.WindowsAzure.MobileServices.MobileServiceClient MobileService { get; set; }
    }
}