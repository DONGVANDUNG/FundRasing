﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Domain.Repositories;
using Abp.Organizations;
using Microsoft.AspNetCore.Mvc;
using esign.Authorization;
using esign.Notifications;
using esign.Organizations;
using esign.Web.Areas.App.Models.Notifications;
using esign.Web.Areas.App.Models.OrganizationUnits;
using esign.Web.Controllers;
using esign.Organizations.Dto;

namespace esign.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class NotificationsController : esignControllerBase
    {
        private readonly INotificationAppService _notificationAppService;
        private readonly IOrganizationUnitAppService _organizationUnitAppService;

        public NotificationsController(
            INotificationAppService notificationAppService, 
            IOrganizationUnitAppService organizationUnitAppService)
        {
            _notificationAppService = notificationAppService;
            _organizationUnitAppService = organizationUnitAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> SettingsModal()
        {
            var notificationSettings = await _notificationAppService.GetNotificationSettings();
            return PartialView("_SettingsModal", notificationSettings);
        }
        
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_MassNotification_Create)]
        public async Task<PartialViewResult> CreateMassNotificationModal()
        {
            var viewModel = new CreateMassNotificationViewModel
            {
                TargetNotifiers = _notificationAppService.GetAllNotifiers()
            };

            return PartialView("_CreateMassNotificationModal", viewModel);
        }
        
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_MassNotification)]
        public PartialViewResult UserLookupTableModal()
        {
            return PartialView("_UserLookupTableModal");
        }
        
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_MassNotification)]
        public async Task<PartialViewResult> OrganizationUnitLookupTableModal()
        {
            var organizationUnits = await _organizationUnitAppService.GetAll();
            var model = new OrganizationUnitLookupTableModel
            {
                AllOrganizationUnits = ObjectMapper.Map<List<OrganizationUnitDto>>(organizationUnits)
            };
            
            return PartialView("_OrganizationUnitLookupTableModal", model);
        }
        
                
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_MassNotification)]
        public ActionResult MassNotifications()
        {
            return View();
        }
    }
}