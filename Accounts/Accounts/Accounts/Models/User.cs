using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Accounts.Models
{
    public class User
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public int RoleID { get; set; }


        public List<classUser> GetUsersForList(string UserName, string EmailAddress)
        {
            DbManager db = new DbManager();
            try
            {
                DataTable dt = new DataTable();
                DataRow[] dr;
                List<classUser> li = new List<classUser>();

                dt = db.ExecuteDataTable("Sp_UserList");
                string whereC = "";

                if (UserName != null && UserName!="")
                {
                    whereC = "UserName like '%" + UserName.ToString() + "%'";
                }
                if (EmailAddress != null && EmailAddress != "")
                {
                    if (whereC == "")
                        whereC = "EmailAddress like '%" + EmailAddress.ToString() + "%'";
                    else
                        whereC = whereC + " and EmailAddress like '%" + EmailAddress.ToString() + "%'";
                }
                dr = dt.Select(whereC);
                foreach (var item in dr)
                {
                    classUser lobj = new classUser();
                    lobj.UserID = (int)item["UserID"];
                    lobj.Username = item["Username"].ToString();
                    lobj.EmailAddress = item["EmailAddress"].ToString();
                    lobj.Password = item["Password"].ToString();
                    li.Add(lobj);
                }
                return li.ToList();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex.Message, ex.StackTrace, ex.Source, "Users GetUsersForList", "");
                return null;
            }
        }

        public DataTable GetUsersForListDT()
        {
            DbManager db = new DbManager();
            try
            {
                DataTable dt = new DataTable();
                dt = db.ExecuteDataTable("Sp_UserList");

                return dt;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex.Message, ex.StackTrace, ex.Source, "Users GetUsersForListDT", "");
                return null;
            }
        }

        public bool IsValid(string _username, string _password)
        {

            DbManager objDBManager = new DbManager();
            bool flag = false;
            DataTable dt = new DataTable();
            objDBManager.AddParameter("@Username", UserName);
            objDBManager.AddParameter("@Password", Password);
            dt = objDBManager.ExecuteDataTable("SP_ValidateUser");
            if (Convert.ToInt32(dt.Rows[0]["ValidUser"]) == 1)
            {

                flag = true;
                System.Web.HttpContext.Current.Session.Add("UserName", _username);
                System.Web.HttpContext.Current.Session.Add("UserID", dt.Rows[0]["UserID"]);
                System.Web.HttpContext.Current.Session.Add("RoleID", dt.Rows[0]["RoleID"]);
            }
            return flag;
        }



        public bool CheckCreditionals(string _username, string _password)
        {

            DbManager objDBManager = new DbManager();
            bool flag = false;
            DataTable dt = new DataTable();
            objDBManager.AddParameter("@Username", _username);
            objDBManager.AddParameter("@Password", _password);
            dt = objDBManager.ExecuteDataTable("SP_ValidateUser");
            if (Convert.ToInt32(dt.Rows[0]["ValidUser"]) == 1)
            {
                flag = true;
            }
            return flag;
        }
        public bool IsUserExists(string _username)
        {

            DbManager objDBManager = new DbManager();
            bool flag = false;
            DataTable dt = new DataTable();
            objDBManager.AddParameter("@Username", _username);
            dt = objDBManager.ExecuteDataTable("SP_IsUserExists");
            if (Convert.ToInt32(dt.Rows[0]["IsUserExists"]) == 1)
            {
                flag = true;
            }
            return flag;
        }
        public string SaveNewUser(classUser objUsr, bool isInsert)
        {
            try
            {
                DbManager objDBManager = new DbManager();
                DataTable dt = new DataTable();
                objDBManager.AddParameter("@Username", objUsr.Username);
                objDBManager.AddParameter("@Deleted", objUsr.Deleted);
                objDBManager.AddParameter("@EmailAddress", objUsr.EmailAddress);
                objDBManager.AddParameter("@RoleID", 0);
                if (isInsert == true)
                {
                    objDBManager.AddParameter("@Password", objUsr.Password);
                    objDBManager.ExecuteDataTable("Sp_UserInfoInsert");
                }
                else
                {
                    if (objUsr.NewPassword == null)
                    {
                        objDBManager.AddParameter("@Password", objUsr.Password);
                    }
                    else
                    {
                        objDBManager.AddParameter("@Password", objUsr.NewPassword);
                    }
                    objDBManager.AddParameter("@UserID", objUsr.UserID);
                    objDBManager.ExecuteDataTable("Sp_UserInfoUpdate");
                }
                return "Saved";
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex.Message, ex.StackTrace, ex.Source, "Users SaveNewUser", "");
                return ex.Message.ToString();
            }
        }

        public classUser LoadEditData(string UserID)
        {
            classUser lobj = new classUser();
            DataTable dt = new DataTable();
            try
            {
                DbManager dbm = new DbManager();
                dt = dbm.ExecuteDataTable("Sp_UserList");
                DataRow[] dr = dt.Select("UserId=" + UserID + "");
                lobj.UserID = (int)dr[0]["UserID"];
                lobj.Username = dr[0]["Username"].ToString();
                lobj.EmailAddress = dr[0]["EmailAddress"].ToString();
                lobj.Password = dr[0]["Password"].ToString();
                return lobj;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex.Message, ex.StackTrace, ex.Source, "Users SaveNewUser", "");
                return null;
            }
        }

        public List<UserPermissions> GetUserPermission(string Status, string UserID)
        {
            DbManager db = new DbManager();
            try
            {
                DataTable dt = new DataTable();
                DataRow[] dr;
                List<UserPermissions> li = new List<UserPermissions>();

                db.AddParameter("@UserID", UserID == null ? "0" : UserID);
                db.AddParameter("@isapproved", Status == null ? "1" : Status);
                dt = db.ExecuteDataTable("Sp_UserRightsIndex");

                dr = dt.Select();
                foreach (DataRow item in dt.Rows)
                {
                    UserPermissions lobj = new UserPermissions();
                    lobj.ID = (int)item["ID"];
                    lobj.MenuDesc = item["MenuDesc"].ToString();
                    lobj.MenuName = item["MenuName"].ToString();
                    lobj.UserID = item["UserID"].ToString();
                    lobj.ControllerName = item["ControllerName"].ToString();
                    lobj.UserName = item["UserName"].ToString();
                    li.Add(lobj);
                }
                return li.ToList();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex.Message, ex.StackTrace, ex.Source, "Users GetUsersForList", "");
                return null;
            }
        }

        public string proUpdateUserPremission(List<int> Ids, string txtUserID, String Status)
        {
            DbManager db = new DbManager();
            DataTable dt = new DataTable();
            try
            {
                ////db.BeginTransaction();
                for (int i = 0; i <= Ids.Count - 1; i++)
                {
                    db = new DbManager();
                    db.AddParameter("@UserID", txtUserID);
                    db.AddParameter("@MenuID", Ids[i]);
                    db.AddParameter("@status", Status);
                    dt = db.ExecuteDataTable("Sp_UserPermissionSettings");
                }
                //db.CommitTransaction();
                return "Saved";
            }
            catch (Exception ex)
            {
                //db.RollBackTransaction();
                ErrorLog.LogError(ex.Message, ex.StackTrace, ex.Source, "Users GetUsersForList", "");
                return ex.Message.ToString();
            }
        }



    }
}