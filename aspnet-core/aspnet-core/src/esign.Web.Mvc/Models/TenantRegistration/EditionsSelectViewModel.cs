using Abp.AutoMapper;
using esign.MultiTenancy.Dto;

namespace esign.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(EditionsSelectOutput))]
    public class EditionsSelectViewModel : EditionsSelectOutput
    {
    }
}
