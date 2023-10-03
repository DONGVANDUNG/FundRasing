using Abp.Application.Services;
using Abp.Application.Services.Dto;
using esign.Authorization.Permissions.Dto;

namespace esign.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
