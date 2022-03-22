using ERP.DatabaseLayer;
using ERP_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class EmployeeController : Controller
    {
        BusinessERP_DBEntities DB = new BusinessERP_DBEntities();
        // GET: Employee
        public ActionResult BranchEmployees()
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

            var employees = DB.tblEmployees.Where(e => e.CompanyID == companyid && e.BranchID == branchid).ToList(); //specific branch k employees
            var list = new List<EmployeeMV>();
            foreach (var employee in employees)
            {
                list.Add(new EmployeeMV()
                {
                    Address = employee.Address,
                    BranchID = employee.BranchID,
                    CNIC = employee.CNIC,
                    CompanyID = employee.CompanyID,
                    ContactNo = employee.ContactNo,
                    Description = employee.Description,
                    Designation = employee.Designation,
                    Email = employee.Email,
                    EmployeeID = employee.EmployeeID,
                    MonthlySalary = employee.MonthlySalary,
                    Name = employee.Name,
                    Photo = employee.Photo,
                    UserID = employee.UserID

                });
            }
            return View(list);
        }
        public ActionResult NewBranchEmployee()
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

            var employee = new EmployeeMV();
            employee.CompanyID = companyid;
            employee.BranchID = branchid;
            employee.UserID = 0;
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewBranchEmployee(EmployeeMV employeemv)
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

            try
            {
                if (ModelState.IsValid)
                {
                    var checkemployee = DB.tblEmployees.Where(e => e.CNIC == employeemv.CNIC.Trim()).FirstOrDefault();//kisi b company mein employee aeek h baar register ho ga.
                    if (checkemployee == null)
                    {
                        var newemployee = new tblEmployee();

                        newemployee.BranchID = branchid;
                        newemployee.CNIC = employeemv.CNIC;
                        newemployee.CompanyID = companyid;
                        newemployee.ContactNo = employeemv.ContactNo;
                        newemployee.Description = employeemv.Description;
                        newemployee.Designation = employeemv.Designation;
                        newemployee.Email = employeemv.Email;
                        newemployee.EmployeeID = employeemv.EmployeeID;
                        newemployee.MonthlySalary = employeemv.MonthlySalary;
                        newemployee.Name = employeemv.Name;
                        newemployee.Photo = "~/Content/Template/img/user/employee.png";
                        newemployee.Address = employeemv.Address;
                        newemployee.UserID = employeemv.UserID;

                        DB.tblEmployees.Add(newemployee);
                        DB.SaveChanges();
                        return RedirectToAction("BranchEmployees");
                    }
                    else
                    {
                        ModelState.AddModelError("CNIC", "Already Exists");
                    }
                }
                return View(employeemv);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Name", "Must fill all fields with correct data!");
                return View(employeemv);
            }
           
        }
    }
}