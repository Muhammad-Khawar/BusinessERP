using ERP.DatabaseLayer;
using ERP_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class CompanyController : Controller
    {
        BusinessERP_DBEntities DB = new BusinessERP_DBEntities();
        // GET: Company
        public ActionResult Companies()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeId"]), out usertypeid);
            
            var listcompanies = new List<CompanyRequestMV>();
            var companies = DB.tblCompanies.ToList();
            foreach (var company in companies)
            {
                var r_company = new CompanyRequestMV();
                r_company.CompanyID = company.CompanyID;
                r_company.Company = company.Name;
                //Branch
                var branch = DB.tblBranches.Where(b => b.CompanyID == r_company.CompanyID).FirstOrDefault();
                r_company.BranchID = branch.BranchID;
                r_company.BranchTypeID = branch.BranchTypeID;

                var branchtype = branch.tblBranchType == null ? "No Type" : branch.tblBranchType.BranchType;

                r_company.BranchType = branchtype;
                r_company.BranchName = branch.BranchName;
                r_company.BranchContact = branch.BranchContact;
                r_company.BranchAddress = branch.BranchAddress;
                //Employee
                var branchemployee = DB.tblEmployees.Where(e => e.BranchID == branch.BranchID && e.CompanyID == company.CompanyID).FirstOrDefault();
                
                r_company.EmployeeID = branchemployee.EmployeeID;
                r_company.EmployeeName = branchemployee.Name;
                r_company.ContactNo = branchemployee.ContactNo;
                r_company.Photo = string.Empty;
                r_company.Email = branchemployee.Email;
                r_company.Address = branchemployee.Address;
                r_company.CNIC = branchemployee.CNIC;
                r_company.Designation = branchemployee.Designation;
                r_company.Description = branchemployee.Description;
                r_company.MonthlySalary = branchemployee.MonthlySalary;
                //Users
                var user = DB.tblUsers.Where(u => u.UserID == branchemployee.UserID).FirstOrDefault();

                r_company.UserID = user.UserID;
                r_company.UserTypeID = user.UserTypeID;

                var usertype = user.tblUserType != null ? user.tblUserType.UserType : "No User Type";
                
                r_company.UserType = usertype;
                r_company.UserName = user.UserName;
                r_company.Password = user.Password;
                r_company.Status = user.IsActive == true ? "Active" : "De-Active";

                listcompanies.Add(r_company);
    }
            
            return View(listcompanies);
        }

        public ActionResult NewCompany()
        {
            var model = new CompanyMV();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewCompany(
            string UserName,
            string Password,
            string CPassword,
            string EName,
            string EContactNo,
            string EEmail,
            string ECNIC,
            string EDesignation,
            float EMonthlySalary,
            string EAddress,
            string CName,
            string BranchName,
            string BranchContact,
            string BranchAddress
            )
        {

            using (var trans = DB.Database.BeginTransaction())
                try
                {
                    if (!string.IsNullOrEmpty(UserName)
                         && !string.IsNullOrEmpty(Password)
                         && !string.IsNullOrEmpty(EName)
                         && !string.IsNullOrEmpty(EContactNo)
                         && !string.IsNullOrEmpty(EEmail)
                         && !string.IsNullOrEmpty(ECNIC)
                         && !string.IsNullOrEmpty(EDesignation)
                         && EMonthlySalary > 0
                         && !string.IsNullOrEmpty(EAddress)
                         && !string.IsNullOrEmpty(CName)
                         && !string.IsNullOrEmpty(BranchName)
                         && !string.IsNullOrEmpty(BranchContact)
                         && !string.IsNullOrEmpty(BranchAddress)
                         )
                    {
                        var checkcompany = DB.tblCompanies.Where(c => c.Name == CName).FirstOrDefault();
                        if(checkcompany!=null)
                        {
                            trans.Rollback();
                            ViewBag.Message = "Company Already Exists!";
                            return View();
                        }
                        var company = new tblCompany()
                        {
                            Name = CName,
                            Logo = string.Empty
                        };

                        DB.tblCompanies.Add(company);
                        DB.SaveChanges();
                        var branch = new tblBranch()
                        {
                            BranchAddress = BranchAddress,
                            BranchContact = BranchContact,
                            BranchName = BranchName,
                            BranchTypeID = 1,
                            CompanyID = company.CompanyID,
                            BrchID = null
                        };

                        DB.tblBranches.Add(branch);
                        DB.SaveChanges();

                        var checkusername = DB.tblUsers.Where(u => u.UserName == UserName).FirstOrDefault();
                        if (checkusername != null)
                        {
                            trans.Rollback();
                            ViewBag.Message = "User Already Exists!";
                            return View();
                        }
                        var user = new tblUser()
                        {
                            ContactNo = EContactNo,
                            Email = EEmail,
                            FullName = EName,
                            IsActive = false,
                            Password = Password,
                            UserName = UserName,
                            UserTypeID = 3, //3 shows Head Office User
                            Address = BranchAddress
                        };

                        DB.tblUsers.Add(user);
                        DB.SaveChanges();

                        var employee = new tblEmployee()
                        {
                            Address = EAddress,
                            BranchID = branch.BranchID,
                            CNIC = ECNIC,
                            CompanyID = company.CompanyID,
                            ContactNo = EContactNo,
                            Designation = EDesignation,
                            Email = EEmail,
                            MonthlySalary = EMonthlySalary,
                            UserID = user.UserID,
                            Name = EName,
                            Description = "Enter Description Here"
                        };

                        DB.tblEmployees.Add(employee);
                        DB.SaveChanges();
                        ViewBag.Message = "Registration Successfully";
                        trans.Commit();
                        return RedirectToAction("Login", "Home");
                    }
                    else
                    {
                        trans.Rollback();
                        ViewBag.Message = "Please Provide Correct Details!";
                        return View("NewCompany");
                    }

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ViewBag.Message = "Please Contact To Adminstrator!";
                    return View();
                }
        }
        public ActionResult Approve(int? userid)
        {
            var user = DB.tblUsers.Find(userid);
            user.IsActive = true;
            DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();
            return RedirectToAction("Companies");
        }
        public ActionResult DeApprove(int? userid)
        {
            var user = DB.tblUsers.Find(userid);
            user.IsActive = false;
            DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();
            return RedirectToAction("Companies");
        }
    }
}