using Abp.AutoMapper;
using esign.Authorization.Users;
using esign.Authorization.Users.Dto;
using esign.Web.Areas.App.Models.Common;

namespace esign.Web.Areas.App.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; set; }
    }
}