using Abp.AutoMapper;
using esign.MultiTenancy;
using esign.MultiTenancy.Dto;
using esign.Web.Areas.App.Models.Common;

namespace esign.Web.Areas.App.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }
    }
}