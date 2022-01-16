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
                        Session["UserID"] = user.UserID;
                        Session["UserName"] = user.UserName;
                        Session["Email"] = user.Email;
                        Session["UserTypeId"] = user.UserTypeID;
                        Session["UserType"] = user.tblUserType.UserType;
                        Session["IsActive"] = Convert.ToBoolean(user.IsActive)==true ? "Active" : "No Active";
                        var usertypeid = user.UserTypeID;
                        if (user.UserTypeID ==1)
                        {
                            return RedirectToAction("Admin", "Dashboard");
                        }
                        else if (usertypeid == 2)
                        {
                            return RedirectToAction("SubAdmin", "Dashboard");
                        }
                        else if (usertypeid == 3)
                        {
                            return RedirectToAction("HeadOffice", "Dashboard");
                        }
                        else if (usertypeid == 4)
                        {
                            return RedirectToAction("HeadOfficeUser", "Dashboard");
                        }
                        else if (usertypeid == 5)
                        {
                            return RedirectToAction("BranchUser", "Dashboard");
                        }
                        else if (usertypeid == 6)
                        {
                            return RedirectToAction("BranchOperator", "Dashboard");
                        }
                    }
                }
            }
            else
            {
                ViewBag.ErrorMsg = "Please Fill the Email and Password";
            }
            Session["UserID"] = string.Empty;
            Session["UserName"] = string.Empty;
            Session["Email"] = string.Empty;
            Session["UserTypeId"] = string.Empty;
            Session["UserType"] = string.Empty;
            Session["IsActive"] = string.Empty;
            return View();
        }
        public ActionResult Logout()
        {
            Session["UserID"] = string.Empty;
            Session["UserName"] = string.Empty;
            Session["Email"] = string.Empty;
            Session["UserTypeId"] = string.Empty;
            Session["UserType"] = string.Empty;
            Session["IsActive"] = string.Empty;
            return RedirectToAction("Login");
        }
    }
}