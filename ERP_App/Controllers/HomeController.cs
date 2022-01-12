using ERP.DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login(string UserEmail, string Password)
        {
            if(!string.IsNullOrEmpty(UserEmail) && !string.IsNullOrEmpty(Password))
            {
                using (BusinessERPSystemEntities db = new BusinessERPSystemEntities())
                {
                    var user = db.tblUsers.Where(u => u.Email == UserEmail && u.Password == Password).FirstOrDefault();
                    if(user == null)
                    {
                        ViewBag.ErrorMsg = "Email or Passsword is incorrect";
                    }
                    else
                    {
                        if(user.UserTypeID ==1)
                        {
                            return RedirectToAction("Dashboard", "Admin");
                        }
                    }
                }
            }
            else
            {
                ViewBag.ErrorMsg = "Please Fill the Email and Password";
            }
            return View();
        }
    }
}