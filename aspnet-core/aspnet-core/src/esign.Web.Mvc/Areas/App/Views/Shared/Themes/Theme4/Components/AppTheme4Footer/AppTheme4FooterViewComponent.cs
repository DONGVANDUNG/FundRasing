﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using esign.Web.Areas.App.Models.Layout;
using esign.Web.Session;
using esign.Web.Views;

namespace esign.Web.Areas.App.Views.Shared.Themes.Theme4.Components.AppTheme4Footer
{
    public class AppTheme4FooterViewComponent : esignViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppTheme4FooterViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footerModel = new FooterViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(footerModel);
        }
    }
}
