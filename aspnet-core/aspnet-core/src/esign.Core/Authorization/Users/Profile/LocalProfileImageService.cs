using System;
using System.Threading.Tasks;
using Abp;
using Abp.Dependency;
using esign.Storage;

namespace esign.Authorization.Users.Profile
{
    public class LocalProfileImageService : IProfileImageService, ITransientDependency
    {
        private readonly UserManager _userManager;

        public LocalProfileImageService(
            UserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetProfilePictureContentForUser(UserIdentifier userIdentifier)
        {
            var user = await _userManager.GetUserOrNullAsync(userIdentifier);
            if (user?.ProfilePictureId == null)
            {
                return "";
            }
             return "";
        }
    }
}