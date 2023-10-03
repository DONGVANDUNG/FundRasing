using System.Threading.Tasks;
using Abp.Application.Services;
using esign.Sessions.Dto;

namespace esign.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
