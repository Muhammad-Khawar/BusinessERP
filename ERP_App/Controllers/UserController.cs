using ERP.DatabaseLayer;
using ERP_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class UserController : Controller
    {
        private BusinessERP_DBEntities DB = new BusinessERP_DBEntities();
        // GET: User
        public ActionResult AllUserTypes()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeId"]), out usertypeid);

            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            var list = new List<UserTypeMV>();
            var usertypes = DB.tblUserTypes.ToList();
            foreach (var usertype in usertypes)
            {
                list.Add(new UserTypeMV { UserTypeID = usertype.UserTypeID, UserType = usertype.UserType });
            }
            return View(list);
        }
        public ActionResult CreateUserType()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeId"]), out usertypeid);

            var usertypemv = new UserTypeMV();
            return View(usertypemv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUserType(UserTypeMV userTypeMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeId"]), out usertypeid);

            if(ModelState.IsValid)
            {
                var checkusertype = DB.tblUserTypes.Where(u => u.UserType == userTypeMV.UserType.Trim()).FirstOrDefault();
                if (checkusertype == null)
                {
                    var usertype = new tblUserType();
                    usertype.UserType = userTypeMV.UserType;
                    DB.tblUserTypes.Add(usertype);
                    DB.SaveChanges();
                    return RedirectToAction("AllUserTypes");
                }
                else
                {
                    ModelState.AddModelError("UserType","Already Exists");
                }
            }
            
            return View();
        }
    }
}