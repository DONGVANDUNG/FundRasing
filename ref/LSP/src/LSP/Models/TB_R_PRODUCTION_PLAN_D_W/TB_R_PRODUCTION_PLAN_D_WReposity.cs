﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;

namespace LSP.Models.TB_R_PRODUCTION_PLAN_D_W
{
    public class TB_R_PRODUCTION_PLAN_D_WReposity : ITB_R_PRODUCTION_PLAN_D_W
    {
        public TB_R_PRODUCTION_PLAN_D_WInfo TB_R_PRODUCTION_PLAN_D_W_Get(string id)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            IList<TB_R_PRODUCTION_PLAN_D_WInfo> list = db.Fetch<TB_R_PRODUCTION_PLAN_D_WInfo>("TB_R_PRODUCTION_PLAN_D_W/TB_R_PRODUCTION_PLAN_D_W_Get", new { id = id });
            db.Close();
            return list.Count > 0 ? list.First() : null;
        }

        public IList<TB_R_PRODUCTION_PLAN_D_WInfo> TB_R_PRODUCTION_PLAN_D_W_Gets(string id)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            IList<TB_R_PRODUCTION_PLAN_D_WInfo> list = db.Fetch<TB_R_PRODUCTION_PLAN_D_WInfo>("TB_R_PRODUCTION_PLAN_D_W/TB_R_PRODUCTION_PLAN_D_W_Gets", new { id = id });
            db.Close();
            return list;
        }

        public IList<TB_R_PRODUCTION_PLAN_D_WInfo> TB_R_PRODUCTION_PLAN_D_W_Search(TB_R_PRODUCTION_PLAN_D_WInfo obj)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            IList<TB_R_PRODUCTION_PLAN_D_WInfo> list = db.Fetch<TB_R_PRODUCTION_PLAN_D_WInfo>("TB_R_PRODUCTION_PLAN_D_W/TB_R_PRODUCTION_PLAN_D_W_Search",
            new
                {
                    CFC = obj.CFC,
                    PROD_SFX = obj.PROD_SFX,
                    PRODUCTION_LINE = obj.PRODUCTION_LINE,
                    WORKING_DATE = obj.WORKING_DATE,
                    W_IN_DATE_PLAN = obj.W_IN_DATE_PLAN,
                    PRODUCTION_MONTH = obj.PRODUCTION_MONTH
                });
            db.Close();
            return list;
        }

        public int TB_R_PRODUCTION_PLAN_D_W_Insert(TB_R_PRODUCTION_PLAN_D_WInfo obj)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            int numrow = db.Execute("TB_R_PRODUCTION_PLAN_D_W/TB_R_PRODUCTION_PLAN_D_W_Insert", new
            {
                CFC = obj.CFC,
                KATASHIKI = obj.KATASHIKI,
                PROD_SFX = obj.PROD_SFX,
                LOT_NO = obj.LOT_NO,
                NO_IN_LOT = obj.NO_IN_LOT,
                BODY_NO = obj.BODY_NO,
                EXT_COLOR = obj.EXT_COLOR,
                VIN_NO = obj.VIN_NO,
                PRODUCTION_LINE = obj.PRODUCTION_LINE,
                SHIFT = obj.SHIFT,
                SEQUENCE_NO = obj.SEQUENCE_NO,
                WORKING_DATE = obj.WORKING_DATE,
                NO_IN_DAY = obj.NO_IN_DAY,
                W_IN_DATE_PLAN = obj.W_IN_DATE_PLAN,
                W_IN_TIME_PLAN = obj.W_IN_TIME_PLAN,
                W_IN_DATE_ACTUAL = obj.W_IN_DATE_ACTUAL,
                W_IN_TIME_ACTUAL = obj.W_IN_TIME_ACTUAL,
                W_OUT_DATE_PLAN = obj.W_OUT_DATE_PLAN,
                W_OUT_TIME_PLAN = obj.W_OUT_TIME_PLAN,
                W_OUT_DATE_ACTUAL = obj.W_OUT_DATE_ACTUAL,
                W_OUT_TIME_ACTUAL = obj.W_OUT_TIME_ACTUAL,
                VERSION_NO = obj.VERSION_NO,
                IS_NQC_PROCESSED = obj.IS_NQC_PROCESSED,
                IS_ACTIVE = obj.IS_ACTIVE,
                CREATED_BY = obj.CREATED_BY,
                CREATED_DATE = obj.CREATED_DATE,
                UPDATED_BY = obj.UPDATED_BY,
                UPDATED_DATE = obj.UPDATED_DATE
            });
            db.Close();
            return numrow;
        }

        public int TB_R_PRODUCTION_PLAN_D_W_Update(TB_R_PRODUCTION_PLAN_D_WInfo obj)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            int numrow = db.Execute("TB_R_PRODUCTION_PLAN_D_W/TB_R_PRODUCTION_PLAN_D_W_Update", new
            {
                id = obj.ID,
                CFC = obj.CFC,
                KATASHIKI = obj.KATASHIKI,
                PROD_SFX = obj.PROD_SFX,
                LOT_NO = obj.LOT_NO,
                NO_IN_LOT = obj.NO_IN_LOT,
                BODY_NO = obj.BODY_NO,
                EXT_COLOR = obj.EXT_COLOR,
                VIN_NO = obj.VIN_NO,
                PRODUCTION_LINE = obj.PRODUCTION_LINE,
                SHIFT = obj.SHIFT,
                SEQUENCE_NO = obj.SEQUENCE_NO,
                WORKING_DATE = obj.WORKING_DATE,
                NO_IN_DAY = obj.NO_IN_DAY,
                W_IN_DATE_PLAN = obj.W_IN_DATE_PLAN,
                W_IN_TIME_PLAN = obj.W_IN_TIME_PLAN,
                W_IN_DATE_ACTUAL = obj.W_IN_DATE_ACTUAL,
                W_IN_TIME_ACTUAL = obj.W_IN_TIME_ACTUAL,
                W_OUT_DATE_PLAN = obj.W_OUT_DATE_PLAN,
                W_OUT_TIME_PLAN = obj.W_OUT_TIME_PLAN,
                W_OUT_DATE_ACTUAL = obj.W_OUT_DATE_ACTUAL,
                W_OUT_TIME_ACTUAL = obj.W_OUT_TIME_ACTUAL,
                VERSION_NO = obj.VERSION_NO,
                IS_NQC_PROCESSED = obj.IS_NQC_PROCESSED,
                IS_ACTIVE = obj.IS_ACTIVE,
                CREATED_BY = obj.CREATED_BY,
                CREATED_DATE = obj.CREATED_DATE,
                UPDATED_BY = obj.UPDATED_BY,
                UPDATED_DATE = obj.UPDATED_DATE
            });
            db.Close();
            return numrow;
        }

        public int TB_R_PRODUCTION_PLAN_D_W_Delete(string id)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            int numrow = db.Execute("TB_R_PRODUCTION_PLAN_D_W/TB_R_PRODUCTION_PLAN_D_W_Delete", new { id = id });
            db.Close();
            return numrow;
        }
    }
}