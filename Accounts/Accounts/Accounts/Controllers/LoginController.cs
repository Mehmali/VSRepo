using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Accounts.Models;
using PagedList;
using System.Web.Script.Serialization;
using System.Data;
using System.Web.Security;


namespace Accounts.Controllers
{
    public class LoginController : Controller
    {
        User objUserDal;
        public ActionResult exceltest()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.User user)
        {
            if (ModelState.IsValid)
            {
                if (user.IsValid(user.UserName, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }
        public ActionResult NewUser()
        {
            if (Permission.IsSessionExpire() == false)
            {
                if (Permission.IsAllowed("NewUser", "Login"))
                {
                    return View();
                }
                else
                {
                    return View("~/Views/Shared/AccessDenied.cshtml");
                }
            }
            else
            {
                return View("~/Views/Shared/AccessDenied.cshtml");
            }
        }
        public ActionResult UsersList(string UserName, string EmailAddress, int? page)
        {
            if (Permission.IsSessionExpire() == false)
            {
                if (Permission.IsAllowed("UsersList", "Login"))
                {
                    objUserDal = new User();
                    int pageSize = 10;
                    int pageNumber = (page ?? 1);
                    if (UserName == "") { UserName = null; }
                    if (EmailAddress == "") { EmailAddress = null; }
                    ViewBag.UserName = UserName;
                    ViewBag.EmailAddress = EmailAddress;
                    return View(objUserDal.GetUsersForList(UserName, EmailAddress).ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    return View("~/Views/Shared/AccessDenied.cshtml");
                }
            }
            else
            {
                return View("~/Views/Shared/AccessDenied.cshtml");
            }
        }

        public JsonResult SaveNewUser(string UserName, string Password, string EmailAddress)
        {
            try
            {
                if (Permission.IsSessionExpire() == false)
                {
                    objUserDal = new User();
                    classUser objUser = new classUser();
                    if (objUserDal.IsUserExists(UserName) == false)
                    {
                        objUser.Username = UserName;
                        objUser.Password = Password;
                        objUser.Deleted = false;
                        objUser.EmailAddress = EmailAddress;
                        if (objUserDal.SaveNewUser(objUser, true) == "Saved")
                        {
                            return new JsonResult() { Data = "Saved", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                        }
                        else
                        {
                            return new JsonResult() { Data = "Error Occured", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                        }
                    }
                    else
                    {
                        return new JsonResult() { Data = "User Exists Please user different user name", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                }
                else
                    return new JsonResult() { Data = "Session Expired", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex.Message, ex.StackTrace, ex.Source, "LoginController SaveNewUser", "");
                return Json(ex.Data.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LoadUserUpdate(string Uid)
        {
            if (Permission.IsSessionExpire() == false)
            {
                Session["User ID"] = Uid;
                return Content("UpdateUser");
            }
            else
            {
                return View("~/Views/Shared/AccessDenied.cshtml");
            }

        }
        public ActionResult UpdateUser()
        {
            if (Permission.IsSessionExpire() == false)
            {
                if (Permission.IsAllowed("UpdateUser", "Login"))
                {
                    ViewBag.UserID = Session["User ID"];
                    return View();
                }
                else
                {
                    return View("~/Views/Shared/AccessDenied.cshtml");
                }
            }
            else
            {
                return View("~/Views/Shared/AccessDenied.cshtml");
            }
        }
        public ActionResult ChangePassword()
        {
            if (Permission.IsSessionExpire() == false)
            {
                if (Permission.IsAllowed("ChangePassword", "Login"))
                {
                    return View();
                }
                else
                {
                    return View("~/Views/Shared/AccessDenied.cshtml");
                }
            }
            else
            {
                return View("~/Views/Shared/AccessDenied.cshtml");
            }
        }


        public JsonResult LoadEditData(string UserID)
        {
            if (Permission.IsSessionExpire() == false)
            {
                objUserDal = new User();
                return new JsonResult() { Data = objUserDal.LoadEditData(UserID), JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            else
                return new JsonResult() { Data = "Session Expired", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult UpdateUserData(string userObj)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                if (Permission.IsSessionExpire() == false)
                {
                    objUserDal = new User();
                    var obj = js.Deserialize(userObj, typeof(classUser));
                    classUser objUser = (classUser)obj;

                    if (objUserDal.SaveNewUser(objUser, false) == "Saved")
                    {
                        return new JsonResult() { Data = "Saved", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                    else
                    {
                        return new JsonResult() { Data = "Error Occured", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                }
                else
                {
                    return new JsonResult() { Data = "Session Expired", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex.Message, ex.StackTrace, ex.Source, "LoginController UpdateUserData", "");
                return Json(ex.Data.ToString(), JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult upDatePassword(string userObj)
        {
            objUserDal = new User();
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                if (Permission.IsSessionExpire() == false)
                {
                    var obj = js.Deserialize(userObj, typeof(classUser));
                    classUser objUser = (classUser)obj;
                    string u = System.Web.HttpContext.Current.Session["UserName"].ToString();
                    objUser.UserID = (int)System.Web.HttpContext.Current.Session["UserID"];
                    if (objUserDal.CheckCreditionals(u, objUser.Password) == true)
                    {
                        if (objUserDal.SaveNewUser(objUser, false) == "Saved")
                        {
                            return new JsonResult() { Data = "Saved", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                        }
                        else
                        {
                            return new JsonResult() { Data = "Error Occure", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                        }
                    }
                    else
                    {
                        return new JsonResult() { Data = "Incorrect Existing password", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                }
                else
                {
                    return new JsonResult() { Data = "Session Expired", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex.Message, ex.StackTrace, ex.Source, "LoingController upDatePassword", "");
                return Json(ex.Data.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Logout()
        {
            try
            {
                FormsAuthentication.SignOut();
                System.Web.HttpContext.Current.Session.Abandon();
                return RedirectToAction("Login", "login");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex.Message, ex.StackTrace, ex.Source, "LoginController Logout", "");
                return new JsonResult() { Data = ex.Message.ToString(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }


        public ActionResult LoadUserRights(string Uid)
        {
            if (Permission.IsSessionExpire() == false)
            {
                Session["RightsUID"] = Uid;
                return Content("UserAccessRights");
            }
            else
            {
                return View("~/Views/Shared/AccessDenied.cshtml");
            }
        }

        public ActionResult UserAccessRights(string txtUserID, string Status, int? page)
        {

            if (Permission.IsSessionExpire() == false)
            {
                if (Permission.IsAllowed("UserAccessRights", "Login"))
                {
                    objUserDal = new User();
                    ViewBag.RightsUID = Session["RightsUID"];
                    ViewBag.Status = (Status ?? "1");
                    ViewBag.tblUsers = new SelectList(objUserDal.GetUsersForList("", "").ToList(), "UserID", "Username");
                    objUserDal = new User();
                    int pageSize = 30;
                    int pageNumber = (page ?? 1);
                    if (txtUserID == null) { Status = null; }
                    return View(objUserDal.GetUserPermission(Status, txtUserID).ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    return View("~/Views/Shared/AccessDenied.cshtml");
                }
            }
            else
            {
                return View("~/Views/Shared/AccessDenied.cshtml");
            }

        }

        public ActionResult ApproveOrRejectALL(List<int> Ids, string txtUserID, String Status, string CurrentStatus)
        {
            int pageNumber = 1;
            objUserDal = new User();
            if (txtUserID == null) { Status = null; }
            try
            {
                if (Permission.IsSessionExpire() == false)
                {
                    if (objUserDal.proUpdateUserPremission(Ids, txtUserID, Status) == "Saved")
                    {
                        TempData["Msg"] = "User Rights are Updated";
                    }
                    else
                    {
                        TempData["Msg"] = "Some Error Occured";
                    }
                    return PartialView("pv_UserAccessRights", objUserDal.GetUserPermission(Status, txtUserID).ToPagedList(pageNumber, 30));
                }
                else
                {
                    return View("~/Views/Shared/AccessDenied.cshtml");
                }

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex.Message, ex.StackTrace, ex.Source, "LoginController ApproveOrRejectALL", "");
                return PartialView("pv_UserAccessRights", objUserDal.GetUserPermission(Status, txtUserID).ToPagedList(pageNumber, 30));
            }

        }

       
    }
}