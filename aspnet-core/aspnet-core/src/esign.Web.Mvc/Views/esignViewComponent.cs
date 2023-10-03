using Abp.AspNetCore.Mvc.ViewComponents;

namespace esign.Web.Views
{
    public abstract class esignViewComponent : AbpViewComponent
    {
        protected esignViewComponent()
        {
            LocalizationSourceName = esignConsts.LocalizationSourceName;
        }
    }
}