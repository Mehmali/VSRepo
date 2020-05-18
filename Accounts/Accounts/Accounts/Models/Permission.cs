using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Accounts.Models
{
    public static class Permission
    {
        public static Boolean IsAllowed(string menuname, string controllername)
        {
            Boolean IsAllow = false;
            DataTable dt = new DataTable();
            try
            {
                if (IsSessionExpire() == false)
                {
                    DbManager objDBManager = new DbManager();
                    objDBManager.AddParameter("@UserId", System.Web.HttpContext.Current.Session["UserId"]);
                    objDBManager.AddParameter("@MenuName", menuname);
                    objDBManager.AddParameter("@ControllerName", controllername);
                    dt = objDBManager.ExecuteDataTable("IsAllowToAccessMenu");
                    if (Convert.ToBoolean(dt.Rows[0]["IsAllow"]))
                    {
                        IsAllow = true;
                    }

                }

                return IsAllow;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool IsSessionExpire()
        {
            bool IsExpire = false;
            if (System.Web.HttpContext.Current.Session["UserID"] == null)
            {
                IsExpire = true;
                FormsAuthentication.SignOut();
                System.Web.HttpContext.Current.Session.Abandon();
            }
            return IsExpire;
        }
    }
}