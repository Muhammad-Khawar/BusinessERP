using ERP.DatabaseLayer;
using ERP_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class AdminConfigController : Controller
    {
        private BusinessERP_DBEntities DB = new BusinessERP_DBEntities();
        
        //  Branch Type Configuration
        //**********************************************************************************
        
        public ActionResult AllBranchTypes()
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
            var list = new List<BranchTypeMV>();
            var branchtypes = DB.tblBranchTypes.ToList();
            foreach (var branchtype in branchtypes)
            {
                list.Add(new BranchTypeMV { BranchTypeID = branchtype.BranchTypeID, BranchType = branchtype.BranchType });
            }
            return View(list);
        }
        public ActionResult CreateBranchType()
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

            var branchtypemv = new BranchTypeMV();
            return View(branchtypemv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBranchType(BranchTypeMV branchTypeMV)
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

            if (ModelState.IsValid)
            {
                var checkbranchtype = DB.tblBranchTypes.Where(u => u.BranchType == branchTypeMV.BranchType.Trim()).FirstOrDefault();
                if (checkbranchtype == null)
                {
                    var branchtype = new tblBranchType();
                    branchtype.BranchType = branchTypeMV.BranchType;
                    DB.tblBranchTypes.Add(branchtype);
                    DB.SaveChanges();
                    return RedirectToAction("AllBranchTypes");
                }
                else
                {
                    ModelState.AddModelError("BranchType", "Already Exists");
                }
            }

            return View();
        }
        public ActionResult EditBranchType(int? branchtypeid)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var userid = 0;
            var UserTypeId = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeId"]), out UserTypeId);
            if (UserTypeId != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }

            var editbranchtype = DB.tblBranchTypes.Find(branchtypeid);

            var branchtypemv = new BranchTypeMV();
            branchtypemv.BranchTypeID = editbranchtype.BranchTypeID;
            branchtypemv.BranchType = editbranchtype.BranchType;
            return View(branchtypemv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBranchType(BranchTypeMV branchTypeMV)
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

            if (ModelState.IsValid)
            {
                var checkbranchtype = DB.tblBranchTypes.Where(u => u.BranchType == branchTypeMV.BranchType.Trim() && u.BranchTypeID != branchTypeMV.BranchTypeID).FirstOrDefault();
                if (checkbranchtype == null)
                {
                    var editbranchtype = new tblBranchType();
                    editbranchtype.BranchTypeID = branchTypeMV.BranchTypeID;
                    editbranchtype.BranchType = branchTypeMV.BranchType;
                    DB.Entry(editbranchtype).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllBranchTypes");
                }
                else
                {
                    ModelState.AddModelError("BranchType", "Already Exists");
                }
            }

            return View();
        }

        //  Branch Type Configuration
        //**********************************************************************************

        public ActionResult AllAccountActivity()
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
            var list = new List<AccountActivityMV>();
            var accountactivities = DB.tblAccountActivities.ToList();
            foreach (var accountactivity in accountactivities)
            {
                list.Add(new AccountActivityMV { AccountActivityID = accountactivity.AccountActivityID, Name = accountactivity.Name });
            }
            return View(list);
        }
    }
}