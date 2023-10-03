using Abp.AutoMapper;
using esign.Authorization.Roles.Dto;
using esign.Web.Areas.App.Models.Common;

namespace esign.Web.Areas.App.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class CreateOrEditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool IsEditMode => Role.Id.HasValue;
    }
}