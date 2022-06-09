using ERP.DatabaseLayer;
using ERP_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class BranchController : Controller
    {
        BusinessERP_DBEntities DB = new BusinessERP_DBEntities();

        public ActionResult AllCompanyBranches()
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
            //kkkkkkkkkkkkkkkkkkkkkkkkkkkk
            var branches = DB.tblBranches.Where(b => b.BrchID == branchid && b.CompanyID == companyid).ToList();
            if (usertypeid == 1)
            {
                branches = DB.tblBranches.ToList();
            }
            //kkkkkkkkkkkkkkkkkkkkkkk
            //var branches = DB.tblBranches.Where(b => b.BrchID == branchid && b.CompanyID == companyid).ToList();
            
            var list = new List<BranchMV>();
            foreach (var branch in branches)
            {
                var addbranch = new BranchMV();
                addbranch.BranchID = branch.BranchID;
                addbranch.BranchTypeID = branch.BranchTypeID;
                addbranch.BranchType = branch.tblBranchType.BranchType;
                addbranch.BranchName = branch.BranchName;
                addbranch.BranchContact = branch.BranchContact;
                addbranch.BranchAddress = branch.BranchAddress;
                addbranch.CompanyID = branch.CompanyID;

                var company = DB.tblCompanies.Find(branch.CompanyID).Name;
                addbranch.Company = company;
                addbranch.BrchID = branch.BrchID;

                var employee = branch.tblEmployees.Where(b => b.Designation.ToLower().Contains("focal")).FirstOrDefault();
                if (employee !=null)
                {
                    var user = DB.tblUsers.Find(employee.UserID);
                    if(user.IsActive == true)
                    {
                        addbranch.IsHaveFocalPerson = true;
                    }
                    else
                    {
                        addbranch.IsHaveFocalPerson = false;
                    }
                }
                else
                {
                    addbranch.IsHaveFocalPerson = false;
                }
                list.Add(addbranch);
            }
            return View(list);
        }
        // GET: Branch
        public ActionResult CreateCompanyBranch()
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

            ViewBag.BranchTypeID = new SelectList(DB.tblBranchTypes.Where(bt => bt.BranchTypeID > branchtypeid), "BranchTypeID", "BranchType", "0");
            var branch = new BranchMV();
            branch.CompanyID = companyid;
            return View(branch);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCompanyBranch(BranchMV branchmv)
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

            if (ModelState.IsValid)
            {
                //aeek area mein aeek zeyada branches ho sakti hein.
                var newbranch = new tblBranch();
                newbranch.BranchName = branchmv.BranchName;
                newbranch.BranchTypeID = branchmv.BranchTypeID;
                newbranch.BranchAddress = branchmv.BranchAddress;
                newbranch.BranchContact = branchmv.BranchContact;
                newbranch.CompanyID = branchmv.CompanyID;
                newbranch.BrchID = branchid; // Os branch k nichay branch create kerni hai.Head branch k nichay. jo user login hai.
                DB.tblBranches.Add(newbranch);
                DB.SaveChanges();
                return RedirectToAction("AllCompanyBranches");
            }
            ViewBag.BranchTypeID = new SelectList(DB.tblBranchTypes.Where(bt => bt.BranchTypeID > branchtypeid), "BranchTypeID", "BranchType", branchmv.BranchTypeID);//agr wrong enter kiya to by default yeh select hona chahiye.
           
            return View(branchmv);
        }

        public ActionResult EditCompanyBranch(int ? branchID)
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

            ViewBag.BranchTypeID = new SelectList(DB.tblBranchTypes.Where(bt => bt.BranchTypeID > branchtypeid), "BranchTypeID", "BranchType", "0");

            var editbranch = DB.tblBranches.Find(branchID);
            
            var branch = new BranchMV();
            
            branch.BranchID = editbranch.BranchID;
            branch.BranchTypeID = editbranch.BranchTypeID;
            branch.BranchType = editbranch.tblBranchType.BranchType;
            branch.BranchName = editbranch.BranchName;
            branch.BranchContact = editbranch.BranchContact;
            branch.BranchAddress = editbranch.BranchAddress;
            branch.CompanyID = editbranch.CompanyID;

            var company = DB.tblCompanies.Find(editbranch.CompanyID).Name;
            branch.Company = company;
            branch.BrchID = editbranch.BrchID;

            ViewBag.BranchTypeID = new SelectList(DB.tblBranchTypes.Where(bt => bt.BranchTypeID > branchtypeid), "BranchTypeID", "BranchType", editbranch.BranchTypeID);

            return View(branch);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCompanyBranch(BranchMV branchmv)
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

            if (ModelState.IsValid)
            {
                //aeek area mein aeek zeyada branches ho sakti hein.
                var editbranch = new tblBranch();
                editbranch.BranchID = branchmv.BranchID;
                editbranch.BranchName = branchmv.BranchName;
                editbranch.BranchTypeID = branchmv.BranchTypeID;
                editbranch.BranchAddress = branchmv.BranchAddress;
                editbranch.BranchContact = branchmv.BranchContact;
                editbranch.CompanyID = branchmv.CompanyID;
                editbranch.BrchID = branchmv.BrchID;
                DB.Entry(editbranch).State = System.Data.Entity.EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("AllCompanyBranches");
            }
            ViewBag.BranchTypeID = new SelectList(DB.tblBranchTypes.Where(bt => bt.BranchTypeID > branchtypeid), "BranchTypeID", "BranchType", branchmv.BranchTypeID);//agr wrong enter kiya to by default yeh select hona chahiye.

            return View(branchmv);
        }
    }
}