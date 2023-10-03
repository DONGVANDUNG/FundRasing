using System.Threading.Tasks;
using Abp.Application.Services;
using esign.Editions.Dto;
using esign.MultiTenancy.Dto;

namespace esign.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}