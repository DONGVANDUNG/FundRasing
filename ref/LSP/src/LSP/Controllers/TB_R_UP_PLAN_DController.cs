﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Toyota.Common.Web.Platform;
using LSP.Models;
using LSP.Models.TB_R_UP_PLAN_D;


namespace LSP.Controllers
{
    public class TB_R_UP_PLAN_DController : PageController
	{
		protected override void Startup()
        {
            Settings.Title = "TB_R_UP_PLAN_D Management";
        }
		
		public ActionResult GridCallback()
        {
            PartialViewResult result = PartialView("_TB_R_UP_PLAN_DList", Session["ObjectInfo"]);
            //Session.Remove("ObjectInfo");
            return result;
        }
		
		public ActionResult TB_R_UP_PLAN_D_Get(string sid)
        {
            return (Json(TB_R_UP_PLAN_DProvider.Instance.TB_R_UP_PLAN_D_Get(sid), JsonRequestBehavior.AllowGet));
        }
		
		public ActionResult SaveData(TB_R_UP_PLAN_DInfo obj)
        {
            bool success = true;
            string message = "";
            try
            {
				string _user = Request.Cookies[CookieFields.COOKIE_NAME][CookieFields.USERNAME];
                obj.CREATED_BY = _user;
                obj.UPDATED_BY = _user;
                if (obj.ID > 0)
                    success = TB_R_UP_PLAN_DProvider.Instance.TB_R_UP_PLAN_D_Update(obj) > 0;
                else
                    success = TB_R_UP_PLAN_DProvider.Instance.TB_R_UP_PLAN_D_Insert(obj) > 0;

                message = success ? "" : "Process fail!";
            }
            catch (Exception ex)
            {
                success = false;
                message = Models.Common.GetErrorMessage(ex);
            }
            return Json(new { success = success, message = message });
        }
		
		public void SetObjectInfo(TB_R_UP_PLAN_DInfo obj)
        {
            Session["ObjectInfo"] = obj;
        }
		
		public ActionResult Delete(string sid)
        {
            bool success = true;
            string message = "";
            try
            {
                success = TB_R_UP_PLAN_DProvider.Instance.TB_R_UP_PLAN_D_Delete(sid) > 0;
                message = success ? "" : "Process fail!";
            }
            catch (Exception ex)
            {
                success = false;
                message = Models.Common.GetErrorMessage(ex);
            }
            return Json(new { success = success, message = message });
        } 
    }
}
