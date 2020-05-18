using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Accounts.Models;
using PagedList;
using System.Web.Script.Serialization;
using System.Data;
namespace Accounts.Controllers
{
    public class HomeController : Controller
    {
        #region ActionResult
        public ActionResult Index()
        {
            if (Permission.IsSessionExpire() == false)
            {
                if (Permission.IsAllowed("Index", "HomeController"))
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
       
        
       
       
       
        

        #endregion
    }
}