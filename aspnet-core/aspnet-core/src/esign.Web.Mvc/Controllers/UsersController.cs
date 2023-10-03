using Abp.AspNetCore.Mvc.Authorization;
using esign.Authorization;
using esign.Storage;
using Abp.BackgroundJobs;
using Abp.Authorization;

namespace esign.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}