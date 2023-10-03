using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using esign.Web.Controllers;

namespace esign.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class WelcomeController : esignControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}