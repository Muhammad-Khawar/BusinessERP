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
                        Session["UserName"] = user.UserName;
                        Session["Email"] = user.Email;
                        Session["UserTypeId"] = user.UserTypeID;
                        Session["UserType"] = user.tblUserType.UserType;
                        Session["IsActive"] = Convert.ToBoolean(user.IsActive)==true ? "Active" : "No Active";
                        if (user.UserTypeID ==1)
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
            Session["UserName"] = string.Empty;
            Session["Email"] = string.Empty;
            Session["UserTypeId"] = string.Empty;
            Session["UserType"] = string.Empty;
            Session["IsActive"] = string.Empty;
            return View();
        }
    }
    public ActionResult Logout()
    {
        Session["UserName"] = string.Empty;
        Session["Email"] = string.Empty;
        Session["UserTypeId"] = string.Empty;
        Session["UserType"] = string.Empty;
        Session["IsActive"] = string.Empty;
        return RedirectToAction("Login");
    }
}