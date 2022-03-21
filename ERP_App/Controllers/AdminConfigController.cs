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

            return View(branchTypeMV);
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

            return View(branchTypeMV);
        }

        // Account Activity Configuration
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
        public ActionResult CreateAccountActivity()
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

            var accountactivitymv = new AccountActivityMV();
            return View(accountactivitymv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccountActivity(AccountActivityMV accountactivitymv)
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
                var checkaccountactivity = DB.tblAccountActivities.Where(u => u.Name == accountactivitymv.Name.Trim()).FirstOrDefault();
                if (checkaccountactivity == null)
                {
                    var accountactivity = new tblAccountActivity();
                    accountactivity.Name = accountactivitymv.Name;
                    DB.tblAccountActivities.Add(accountactivity);
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountActivity");
                }
                else
                {
                    ModelState.AddModelError("Name", "Already Exists");
                }
            }

            return View(accountactivitymv);
        }
        public ActionResult EditAccountActivity(int? accountactivityid)
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

            var editaccountactivity = DB.tblAccountActivities.Find(accountactivityid);

            var accountactivitymv = new AccountActivityMV();
            accountactivitymv.AccountActivityID = editaccountactivity.AccountActivityID;
            accountactivitymv.Name = editaccountactivity.Name;
            return View(accountactivitymv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccountActivity(AccountActivityMV accountactivitymv)
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
                var checkaccountactivity = DB.tblAccountActivities.Where(u => u.Name == accountactivitymv.Name.Trim() && u.AccountActivityID != accountactivitymv.AccountActivityID).FirstOrDefault();
                if (checkaccountactivity == null)
                {
                    var editaccountactivity = new tblAccountActivity();
                    editaccountactivity.AccountActivityID = accountactivitymv.AccountActivityID;
                    editaccountactivity.Name = accountactivitymv.Name;
                    DB.Entry(editaccountactivity).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountActivity");
                }
                else
                {
                    ModelState.AddModelError("Name", "Already Exists");
                }
            }

            return View(accountactivitymv);
        }
        // Account Heads Configuration
        //**********************************************************************************

        public ActionResult AllAccountHeads()
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
            var list = new List<AccountHeadMV>();
            var accountheads = DB.tblAccountHeads.ToList();
            foreach (var accounthead in accountheads)
            {
                var addaccounthead = new AccountHeadMV();
                addaccounthead.AccountHeadID = accounthead.AccountHeadID;
                addaccounthead.AccountHeadName = accounthead.AccountHeadName;
                addaccounthead.Code = accounthead.Code;
                addaccounthead.UserID = accounthead.UserID;
                addaccounthead.CreatedBy = accounthead.tblUser.UserName;
                
                list.Add(addaccounthead);
            }
            return View(list);
        }
        public ActionResult CreateAccountHead()
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

            var accountheadmv = new AccountHeadMV();
            accountheadmv.UserID = userid;
            return View(accountheadmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccountHead(AccountHeadMV accountheadmv)
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
                var checkaccounthead = DB.tblAccountHeads.Where(u => u.AccountHeadName == accountheadmv.AccountHeadName.Trim()).FirstOrDefault();
                if (checkaccounthead == null)
                {
                    var accounthead = new tblAccountHead();
                    accounthead.AccountHeadName = accountheadmv.AccountHeadName;
                    accounthead.Code = accountheadmv.Code;
                    accounthead.UserID = userid;  //kis user ne esay create kiya hai.
                    DB.tblAccountHeads.Add(accounthead);
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountHeads");
                }
                else
                {
                    ModelState.AddModelError("AccountHeadName", "Already Exists");
                }
            }

            return View(accountheadmv);
        }

        public ActionResult EditAccountHead(int? accountheadid)
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

            var editaccounthead = DB.tblAccountHeads.Find(accountheadid);

            var accountheadmv = new AccountHeadMV();
            accountheadmv.AccountHeadID = editaccounthead.AccountHeadID;
            accountheadmv.AccountHeadName = editaccounthead.AccountHeadName;
            accountheadmv.Code = editaccounthead.Code;
            accountheadmv.UserID = userid;
            return View(accountheadmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccountHead(AccountHeadMV accountheadmv)
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
                var checkaccounthead = DB.tblAccountHeads.Where(u => u.AccountHeadName == accountheadmv.AccountHeadName.Trim() && u.AccountHeadID != accountheadmv.AccountHeadID).FirstOrDefault();
                if (checkaccounthead == null)
                {
                    var editaccounthead = new tblAccountHead();
                    editaccounthead.AccountHeadID = accountheadmv.AccountHeadID;
                    editaccounthead.AccountHeadName = accountheadmv.AccountHeadName;
                    editaccounthead.Code = accountheadmv.Code;
                    editaccounthead.UserID = userid;
                    DB.Entry(editaccounthead).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountHeads");
                }
                else
                {
                    ModelState.AddModelError("AccountHeadName", "Already Exists");
                }
            }

            return View(accountheadmv);
        }

        // Account Financial Years Configuration
        //**********************************************************************************

        public ActionResult AllFinancialYears()
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
            var list = new List<FinancialYearMV>();
            var financialyears = DB.tblFinancialYears.ToList();
            foreach (var financialyear in financialyears)
            {
                var addfinancialyear = new FinancialYearMV();
                addfinancialyear.FinancialYearID = financialyear.FinancialYearID;
                addfinancialyear.FinancialYear = financialyear.FinancialYear;
                addfinancialyear.StartDate = financialyear.StartDate;
                addfinancialyear.EndDate = financialyear.EndDate;
                addfinancialyear.IsActive = financialyear.IsActive;
                addfinancialyear.UserID = financialyear.UserID;
                addfinancialyear.CreatedBy = financialyear.tblUser.UserName;

                list.Add(addfinancialyear);
            }
            return View(list);
        }
        public ActionResult CreateFinancialYear()
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

            var financialyearmv = new FinancialYearMV();
            financialyearmv.UserID = userid;
            financialyearmv.StartDate = DateTime.Now;
            financialyearmv.EndDate = DateTime.Now.AddDays(365);
            return View(financialyearmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFinancialYear(FinancialYearMV financialyearmv)
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
                var checkfinancialyear = DB.tblFinancialYears.Where(u => u.FinancialYear == financialyearmv.FinancialYear.Trim()).FirstOrDefault();
                if (checkfinancialyear == null)
                {
                    var financialyear = new tblFinancialYear();
                    financialyear.FinancialYearID = financialyearmv.FinancialYearID;
                    financialyear.FinancialYear = financialyearmv.FinancialYear;
                    financialyear.StartDate = financialyearmv.StartDate;
                    financialyear.EndDate = financialyearmv.EndDate;  //kis user ne esay create kiya hai.
                    financialyear.IsActive = financialyearmv.IsActive;
                    financialyear.UserID = userid;
                    
                    DB.tblFinancialYears.Add(financialyear);
                    DB.SaveChanges();
                    return RedirectToAction("AllFinancialYears");
                }
                else
                {
                    ModelState.AddModelError("FinancialYear", "Already Exists");
                }
            }

            return View(financialyearmv);
        }
        public ActionResult EditFinancialYear(int? financialyearid)
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

            var financialyear = DB.tblFinancialYears.Find(financialyearid);

            var editfinancialyearmv = new FinancialYearMV();
            editfinancialyearmv.FinancialYearID = financialyear.FinancialYearID;
            editfinancialyearmv.FinancialYear = financialyear.FinancialYear;
            editfinancialyearmv.StartDate = financialyear.StartDate;
            editfinancialyearmv.EndDate = financialyear.EndDate;  //kis user ne esay create kiya hai.
            editfinancialyearmv.IsActive = financialyear.IsActive;
            editfinancialyearmv.UserID = userid;
            return View(editfinancialyearmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFinancialYear(FinancialYearMV financialyearmv)
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
                var checkfinancialyear = DB.tblFinancialYears.Where(u => u.FinancialYear == financialyearmv.FinancialYear.Trim() && u.FinancialYearID != financialyearmv.FinancialYearID).FirstOrDefault();
                if (checkfinancialyear == null)
                {
                    var editfinancialyear = new tblFinancialYear();
                    editfinancialyear.FinancialYearID = financialyearmv.FinancialYearID;
                    editfinancialyear.FinancialYear = financialyearmv.FinancialYear;
                    editfinancialyear.StartDate = financialyearmv.StartDate;
                    editfinancialyear.EndDate = financialyearmv.EndDate;  //kis user ne esay create kiya hai.
                    editfinancialyear.IsActive = financialyearmv.IsActive;
                    editfinancialyear.UserID = userid;
                    DB.Entry(editfinancialyear).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllFinancialYears");
                }
                else
                {
                    ModelState.AddModelError("FinancialYear", "Already Exists");
                }
            }

            return View(financialyearmv);
        }
    }
}