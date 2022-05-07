using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP.DatabaseLayer;
using ERP_App.Models;

namespace ERP_App.Controllers
{
    public class AccountsController : Controller
    {
        private BusinessERP_DBEntities DB = new BusinessERP_DBEntities();

        // GET: Accounts
        public ActionResult AllAccountControls()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            //Yeh ab Admin nahi keray ga, bel-k comapny khod keray gi. es liye zeyada validation add hon gi.
            var companyid = 0;
            var branchid = 0;
            var branchtypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeId"]), out usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out branchtypeid);

            var list = new List<AccountControlMV>();
            var tblAccountControls = DB.tblAccountControls.Where(a => a.CompanyID==companyid && a.BranchID == branchid).ToList();
            foreach (var account in tblAccountControls)
            {
                var accountcontrol = new AccountControlMV();

                accountcontrol.AccountControlID = account.AccountControlID;
                accountcontrol.AccountHeadID = account.AccountHeadID;

                var accounthead = DB.tblAccountHeads.Find(account.AccountHeadID);
                accountcontrol.AccountHead = accounthead.AccountHeadName;
                accountcontrol.AccountControlName = account.AccountControlName;
                accountcontrol.UserID = account.UserID;
                accountcontrol.CreatedBy = account.tblUser.UserName;
                list.Add(accountcontrol);
    }
            return View(list);
        }
        [HttpGet]
        public JsonResult GetAccountControl(int? accountheadID)
        {
           
            var userid = 0;
            var usertypeid = 0;
            //Yeh ab Admin nahi keray ga, bel-k comapny khod keray gi. es liye zeyada validation add hon gi.
            var companyid = 0;
            var branchid = 0;
            var branchtypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeId"]), out usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out branchtypeid);

            var list = new List<AccountControlMV>();
            var tblAccountControls = DB.tblAccountControls.Where(a => a.CompanyID == companyid && a.BranchID == branchid && a.AccountHeadID == accountheadID).ToList();
            foreach (var account in tblAccountControls)
            {
                var accountcontrol = new AccountControlMV();

                accountcontrol.AccountControlID = account.AccountControlID;
                accountcontrol.AccountHeadID = account.AccountHeadID;

                var accounthead = DB.tblAccountHeads.Find(account.AccountHeadID);
                accountcontrol.AccountHead = accounthead.AccountHeadName;
                accountcontrol.AccountControlName = account.AccountControlName;
                accountcontrol.UserID = account.UserID;
                accountcontrol.CreatedBy = account.tblUser.UserName;
                list.Add(accountcontrol);
            }
                return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAccountSubControl(int? accountControlID)
        {

            var userid = 0;
            var usertypeid = 0;
            //Yeh ab Admin nahi keray ga, bel-k comapny khod keray gi. es liye zeyada validation add hon gi.
            var companyid = 0;
            var branchid = 0;
            var branchtypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeId"]), out usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out branchtypeid);

            var list = new List<AccountSubControlMV>();
            var tblAccountSubControls = DB.tblAccountSubControls.Where(a => a.CompanyID == companyid && a.BranchID == branchid && a.AccountControlID == accountControlID).ToList();
            foreach (var account in tblAccountSubControls)
            {
                var accountcontrol = new AccountSubControlMV();

                accountcontrol.AccountSubControlID = account.AccountSubControlID;
                accountcontrol.AccountSubControlName = account.AccountSubControlName;
                list.Add(accountcontrol);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: Accounts/Create
        public ActionResult CreateAccountControl()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            //Yeh ab Admin nahi keray ga, bel-k comapny khod keray gi. es liye zeyada validation add hon gi.
            var companyid = 0;
            var branchid = 0;
            var branchtypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeId"]), out usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out branchtypeid);

            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads, "AccountHeadID", "AccountHeadName","0");
           

            var account = new AccountControlMV();

            return View(account);
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccountControl(AccountControlMV accountControlMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            //Yeh ab Admin nahi keray ga, bel-k comapny khod keray gi. es liye zeyada validation add hon gi.
            var companyid = 0;
            var branchid = 0;
            var branchtypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeId"]), out usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out branchtypeid);

            accountControlMV.CompanyID = companyid;
            accountControlMV.BranchID = branchid;
            accountControlMV.UserID = userid;
            if (ModelState.IsValid)
            {
                var checkaccountcontrol = DB.tblAccountControls.Where(a =>a.AccountHeadID == accountControlMV.AccountHeadID && a.AccountControlName == accountControlMV.AccountControlName && a.CompanyID == companyid && a.BranchID == branchid).FirstOrDefault();
                if(checkaccountcontrol == null)
                {
                    var newaccountcontrol = new tblAccountControl();

                    newaccountcontrol.CompanyID = companyid;
                    newaccountcontrol.BranchID = branchid;
                    newaccountcontrol.AccountHeadID = accountControlMV.AccountHeadID;
                    newaccountcontrol.AccountControlName = accountControlMV.AccountControlName;
                    newaccountcontrol.UserID = userid;
                     
                     DB.tblAccountControls.Add(newaccountcontrol);
                     DB.SaveChanges();
                    return RedirectToAction("AllAccountControls");
                }
                else
                {
                    ModelState.AddModelError("AccountControlName", "Already exists");
                }
            }

            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads, "AccountHeadID", "AccountHeadName",accountControlMV.AccountHeadID);
            return View(accountControlMV);
        }

        // GET: Accounts/Edit/5
        public ActionResult EditAccountControl(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            //Yeh ab Admin nahi keray ga, bel-k comapny khod keray gi. es liye zeyada validation add hon gi.
            var companyid = 0;
            var branchid = 0;
            var branchtypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeId"]), out usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out branchtypeid);

           
            var tblAccountControl = DB.tblAccountControls.Find(id);
            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads, "AccountHeadID", "AccountHeadName", tblAccountControl.AccountHeadID);

            var account = new AccountControlMV();
            account.AccountControlID = tblAccountControl.AccountControlID;
            account.AccountControlName = tblAccountControl.AccountControlName;
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccountControl(AccountControlMV accountControlMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            //Yeh ab Admin nahi keray ga, bel-k comapny khod keray gi. es liye zeyada validation add hon gi.
            var companyid = 0;
            var branchid = 0;
            var branchtypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeId"]), out usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out branchtypeid);

            accountControlMV.CompanyID = companyid;
            accountControlMV.BranchID = branchid;
            accountControlMV.UserID = userid;

            if (ModelState.IsValid)
            {
                var checkaccountcontrol = DB.tblAccountControls.Where(a => a.AccountHeadID == accountControlMV.AccountHeadID && a.AccountControlID!= accountControlMV.AccountControlID && a.AccountControlName == accountControlMV.AccountControlName && a.CompanyID == companyid && a.BranchID == branchid).FirstOrDefault();
                if (checkaccountcontrol == null)
                {
                    var editaccountcontrol = DB.tblAccountControls.Find(accountControlMV.AccountControlID);
                    editaccountcontrol.AccountControlName = accountControlMV.AccountControlName;
                    editaccountcontrol.AccountHeadID = accountControlMV.AccountHeadID;
                    editaccountcontrol.UserID = userid;
                    DB.Entry(editaccountcontrol).State = EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountControls");
                }
                else
                {
                    ModelState.AddModelError("AccountControlName", "Already exists");
                }
            }
            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads, "AccountHeadID", "AccountHeadName", accountControlMV.AccountHeadID);
            return View(accountControlMV);
        }
        public ActionResult AllAccountSubControls()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            //Yeh ab Admin nahi keray ga, bel-k comapny khod keray gi. es liye zeyada validation add hon gi.
            var companyid = 0;
            var branchid = 0;
            var branchtypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeId"]), out usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out branchtypeid);

            var list = new List<AccountSubControlMV>();
            var tblAccountSubControlslist = DB.tblAccountSubControls.Where(a => a.CompanyID == companyid && a.BranchID == branchid).ToList();
            foreach (var account in tblAccountSubControlslist)
            {
                var accountsubcontrol = new AccountSubControlMV();

                accountsubcontrol.AccountSubControlID = account.AccountSubControlID;
                accountsubcontrol.AccountSubControlName = account.AccountSubControlName;
                accountsubcontrol.AccountControlID = account.AccountControlID;
                accountsubcontrol.AccountHeadID = account.AccountHeadID;

                var accounthead = DB.tblAccountHeads.Find(account.AccountHeadID);
                accountsubcontrol.AccountHead = accounthead.AccountHeadName;
                accountsubcontrol.AccountControl = account.tblAccountControl.AccountControlName;
                accountsubcontrol.UserID = account.UserID;
                accountsubcontrol.CreatedBy = account.tblUser.UserName;
                list.Add(accountsubcontrol);
            }
            return View(list);
        }
        // GET: Accounts/Create
        public ActionResult CreateAccountSubControl()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            //Yeh ab Admin nahi keray ga, bel-k comapny khod keray gi. es liye zeyada validation add hon gi.
            var companyid = 0;
            var branchid = 0;
            var branchtypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeId"]), out usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out branchtypeid);

            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads.ToList(), "AccountHeadID", "AccountHeadName", "0");
            ViewBag.AccountControlID = new SelectList(DB.tblAccountControls.Where(c=>c.BranchID == branchid && c.CompanyID == companyid).ToList(), "AccountControlID", "AccountControlName", "0");
            
            var account = new AccountSubControlMV();

            return View(account);
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccountSubControl(AccountSubControlMV accountsubControlMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            //Yeh ab Admin nahi keray ga, bel-k comapny khod keray gi. es liye zeyada validation add hon gi.
            var companyid = 0;
            var branchid = 0;
            var branchtypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeId"]), out usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out branchtypeid);

            accountsubControlMV.CompanyID = companyid;
            accountsubControlMV.BranchID = branchid;
            accountsubControlMV.UserID = userid;
            if (ModelState.IsValid)
            {
                var checkaccountsubcontrol = DB.tblAccountSubControls.Where(a => a.AccountHeadID == accountsubControlMV.AccountHeadID && a.AccountSubControlName == accountsubControlMV.AccountSubControlName && a.AccountControlID == accountsubControlMV.AccountControlID && a.CompanyID == companyid && a.BranchID == branchid).FirstOrDefault();
                if (checkaccountsubcontrol == null)
                {
                    var newaccountsubcontrol = new tblAccountSubControl();

                    newaccountsubcontrol.CompanyID = companyid;
                    newaccountsubcontrol.BranchID = branchid;
                    newaccountsubcontrol.AccountHeadID = accountsubControlMV.AccountHeadID;
                    newaccountsubcontrol.AccountControlID = accountsubControlMV.AccountControlID;
                    newaccountsubcontrol.AccountSubControlName = accountsubControlMV.AccountSubControlName;
                    newaccountsubcontrol.UserID = userid;

                    DB.tblAccountSubControls.Add(newaccountsubcontrol);
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountSubControls");
                }
                else
                {
                    ModelState.AddModelError("AccountSubControlName", "Already exists");
                }
            }

            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads, "AccountHeadID", "AccountHeadName", accountsubControlMV.AccountHeadID);
            ViewBag.AccountControlID = new SelectList(DB.tblAccountControls.Where(c => c.BranchID == branchid && c.CompanyID == companyid).ToList(), "AccountControlID", "AccountControlName", accountsubControlMV.AccountControlID);
            return View(accountsubControlMV);
        }

        public ActionResult EditAccountSubControl(int ? accountsubcontrolID)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            //Yeh ab Admin nahi keray ga, bel-k comapny khod keray gi. es liye zeyada validation add hon gi.
            var companyid = 0;
            var branchid = 0;
            var branchtypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeId"]), out usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out branchtypeid);

            var accountsubcontrol = DB.tblAccountSubControls.Find(accountsubcontrolID);
            var accountSubControlMV = new AccountSubControlMV();
            accountSubControlMV.AccountSubControlID = accountsubcontrol.AccountSubControlID;
            accountSubControlMV.AccountHeadID = accountsubcontrol.AccountHeadID;
            accountSubControlMV.AccountControlID = accountsubcontrol.AccountControlID;
            accountSubControlMV.AccountSubControlName = accountsubcontrol.AccountSubControlName;

            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads.ToList(), "AccountHeadID", "AccountHeadName", accountSubControlMV.AccountHeadID);
            ViewBag.AccountControlID = new SelectList(DB.tblAccountControls.Where(c => c.BranchID == branchid && c.CompanyID == companyid && c.AccountHeadID == accountSubControlMV.AccountHeadID).ToList(), "AccountControlID", "AccountControlName", accountSubControlMV.AccountControlID);

            var account = new AccountSubControlMV();

            return View(accountSubControlMV);
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccountSubControl(AccountSubControlMV accountsubControlMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            //Yeh ab Admin nahi keray ga, bel-k comapny khod keray gi. es liye zeyada validation add hon gi.
            var companyid = 0;
            var branchid = 0;
            var branchtypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeId"]), out usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out branchtypeid);

            accountsubControlMV.CompanyID = companyid;
            accountsubControlMV.BranchID = branchid;
            accountsubControlMV.UserID = userid;
            if (ModelState.IsValid)
            {
                var checkaccountsubcontrol = DB.tblAccountSubControls.Where(a => a.AccountHeadID == accountsubControlMV.AccountHeadID && a.AccountSubControlName == accountsubControlMV.AccountSubControlName && a.AccountControlID == accountsubControlMV.AccountControlID && a.CompanyID == companyid && a.BranchID == branchid && a.AccountSubControlID != accountsubControlMV.AccountSubControlID).FirstOrDefault();
                if (checkaccountsubcontrol == null)
                {
                    var editaccountsubcontrol = DB.tblAccountSubControls.Find(accountsubControlMV.AccountSubControlID);
                  
                    editaccountsubcontrol.AccountHeadID = accountsubControlMV.AccountHeadID;
                    editaccountsubcontrol.AccountControlID = accountsubControlMV.AccountControlID;
                    editaccountsubcontrol.AccountSubControlName = accountsubControlMV.AccountSubControlName;
                    editaccountsubcontrol.UserID = userid;

                    DB.Entry(editaccountsubcontrol).State = EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountSubControls");
                }
                else
                {
                    ModelState.AddModelError("AccountSubControlName", "Already exists");
                }
            }

            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads, "AccountHeadID", "AccountHeadName", accountsubControlMV.AccountHeadID);
            ViewBag.AccountControlID = new SelectList(DB.tblAccountControls.Where(c => c.BranchID == branchid && c.CompanyID == companyid && c.AccountHeadID == accountsubControlMV.AccountHeadID).ToList(), "AccountControlID", "AccountControlName", accountsubControlMV.AccountControlID);
            return View(accountsubControlMV);
        }
    }
}
