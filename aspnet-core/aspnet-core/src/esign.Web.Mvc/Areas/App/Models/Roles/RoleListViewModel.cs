using System.Collections.Generic;
using Abp.Application.Services.Dto;
using esign.Authorization.Permissions.Dto;
using esign.Web.Areas.App.Models.Common;

namespace esign.Web.Areas.App.Models.Roles
{
    public class RoleListViewModel : IPermissionsEditViewModel
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}