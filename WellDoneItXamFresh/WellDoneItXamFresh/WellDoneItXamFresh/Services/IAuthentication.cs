using System;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace WellDoneIt.Services
{
    public interface IAuthentication
    {
        Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider);
        Task LogoutAsync(MobileServiceClient client);
        void ClearCookies();
    }
}

