using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using esign.Web.Areas.App.Models.Layout;
using esign.Web.Session;
using esign.Web.Views;

namespace esign.Web.Areas.App.Views.Shared.Themes.Theme7.Components.AppTheme7Brand
{
    public class AppTheme7BrandViewComponent : esignViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppTheme7BrandViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerModel = new HeaderViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync(),
            };

            return View(headerModel);
        }
    }
}
