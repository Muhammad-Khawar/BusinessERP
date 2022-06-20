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
            //kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk
            var employees = DB.tblEmployees.Where(e => e.CompanyID == companyid && e.BranchID == branchid).ToList();
            if (usertypeid == 1)
            {
                employees = DB.tblEmployees.ToList();
            }
            //kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk
            
            //var employees = DB.tblEmployees.Where(e => e.CompanyID == companyid && e.BranchID == branchid).ToList(); //specific branch k employees
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
                        //kkkkkkkkkkkkkkkkkkkkkkkkkkkkkk
                        employeemv.Pro_Pic.SaveAs(Server.MapPath("~/ProductPicture/" + employeemv.Pro_Pic.FileName));
                        newemployee.Photo = "~/ProductPicture/" + employeemv.Pro_Pic.FileName;
                        //kkkkkkkkkkkkkkkkkkkkkkkkkkkkkk

                        //newemployee.Photo = "~/Content/Template/img/user/employee.png";
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
        public ActionResult EditBranchEmployee(int? employeeid)
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

            var employee = DB.tblEmployees.Find(employeeid);

            var editemployee = new EmployeeMV();
            editemployee.Address = employee.Address;
            editemployee.BranchID = employee.BranchID;
            editemployee.CNIC = employee.CNIC;
            editemployee.CompanyID = employee.CompanyID;
            editemployee.ContactNo = employee.ContactNo;
            editemployee.Description = employee.Description;
            editemployee.Designation = employee.Designation;
            editemployee.Email = employee.Email;
            editemployee.EmployeeID = employee.EmployeeID;
            editemployee.MonthlySalary = employee.MonthlySalary;
            editemployee.Name = employee.Name;
            editemployee.Photo = employee.Photo;
            editemployee.UserID = employee.UserID;
            return View(editemployee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBranchEmployee(EmployeeMV employeemv)
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
                    //var checkemployee = DB.tblEmployees.Where(e => e.CNIC == employeemv.CNIC.Trim() && e.EmployeeID == employeemv.EmployeeID).FirstOrDefault();//kisi b company mein employee aeek h baar register ho ga.
                    //if (checkemployee == null)
                    //{
                        var editemployee = DB.tblEmployees.Where(x=>x.CompanyID==companyid && x.BranchID == branchid && x.EmployeeID == employeemv.EmployeeID).FirstOrDefault();
                        editemployee.CNIC = employeemv.CNIC;
                        editemployee.ContactNo = employeemv.ContactNo;
                        editemployee.Description = employeemv.Description;
                        editemployee.Designation = employeemv.Designation;
                        editemployee.Email = employeemv.Email;
                        editemployee.EmployeeID = employeemv.EmployeeID;
                        editemployee.MonthlySalary = employeemv.MonthlySalary;
                        editemployee.Name = employeemv.Name;
                        //kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk
                        if (employeemv.Pro_Pic != null)
                        {
                            employeemv.Pro_Pic.SaveAs(Server.MapPath("~/ProductPicture/" + employeemv.Pro_Pic.FileName));
                            editemployee.Photo = "~/ProductPicture/" + employeemv.Pro_Pic.FileName;
                        }
                        //kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk

                        //  newemployee.Photo = "~/Content/Template/img/user/employee.png";
                        editemployee.Address = employeemv.Address;

                        DB.Entry(editemployee).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                        return RedirectToAction("BranchEmployees");
                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("CNIC", "Already Exists");
                    //}
                }
                return View(employeemv);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Name", "Must fill all fields with correct data!");
                return View(employeemv);
            }

        }

        public ActionResult ShowBranchFocalPerson(int? BranchID)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var focalperson = new FocalPersonMV();
            focalperson.employeeMV = new EmployeeMV();
            focalperson.userMV = new UserMV();
            var employees = DB.tblEmployees.Where(e => e.BranchID == BranchID && e.Designation.Contains("Focal"));

            foreach (var employee in employees)
            {
                var user = DB.tblUsers.Where(u => u.UserID == employee.UserID && u.IsActive ==true).FirstOrDefault();
                if(user != null)
                {
                    focalperson.CompanyID = employee.CompanyID;
                    focalperson.BranchID = employee.BranchID;
                    //Employee
                    focalperson.employeeMV.Name = employee.Name;
                    focalperson.employeeMV.ContactNo = employee.ContactNo;
                    focalperson.employeeMV.Photo = employee.Photo;
                    focalperson.employeeMV.Email = employee.Email;
                    focalperson.employeeMV.Address = employee.Address;
                    focalperson.employeeMV.CNIC = employee.CNIC;
                    focalperson.employeeMV.Designation = employee.Designation;
                    focalperson.employeeMV.Description = employee.Description;
                    focalperson.employeeMV.MonthlySalary = employee.MonthlySalary;
                    //User
                    focalperson.userMV.UserID = user.UserID;
                    focalperson.userMV.UserType = user.tblUserType.UserType;
                    focalperson.userMV.FullName = user.FullName;
                    focalperson.userMV.Email = user.Email;
                    focalperson.userMV.ContactNo = user.ContactNo;
                    focalperson.userMV.UserName = user.UserName;
                    focalperson.userMV.IsActive = user.IsActive;
                    focalperson.userMV.Address = user.Address;
                }
            }
            return View(focalperson);
        }
        public ActionResult CreateBranchFocalPerson(int? BranchID)
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

            var branch = DB.tblBranches.Find(BranchID);

            var focalpersonmv = new FocalPersonMV();
            focalpersonmv.employeeMV = new EmployeeMV();
            focalpersonmv.userMV = new UserMV();
            focalpersonmv.BranchID = branch.BranchID;
            focalpersonmv.CompanyID = branch.CompanyID;
            //ViewBag.UserTypeID = new SelectList(DB.tblUserTypes.ToList(), "UserTypeID", "UserType", focalpersonmv.userMV.UserTypeID);
            return View(focalpersonmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBranchFocalPerson(FocalPersonMV focalpersonmv)
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

            using (var transaction = DB.Database.BeginTransaction())
            {
                try
            {
                if (ModelState.IsValid)
                {       
                        var newuser = new tblUser();
                        newuser.UserTypeID = 3;
                        newuser.FullName = focalpersonmv.employeeMV.Name;
                        newuser.Email = focalpersonmv.employeeMV.Email;
                        newuser.ContactNo = focalpersonmv.employeeMV.ContactNo;
                        newuser.UserName = focalpersonmv.userMV.UserName;
                        newuser.Password = focalpersonmv.userMV.Password;
                        newuser.IsActive = true;
                        newuser.Address = focalpersonmv.employeeMV.Address;
                        
                        DB.tblUsers.Add(newuser);
                        DB.SaveChanges();

                        //Employee
                        var newemployee = new tblEmployee();
                        newemployee.BranchID = focalpersonmv.BranchID;
                        newemployee.CNIC = focalpersonmv.employeeMV.CNIC;
                        newemployee.CompanyID = focalpersonmv.CompanyID;
                        newemployee.ContactNo = focalpersonmv.employeeMV.ContactNo;
                        newemployee.Description = focalpersonmv.employeeMV.Description;
                        newemployee.Designation = focalpersonmv.employeeMV.Designation;
                        newemployee.Email = focalpersonmv.employeeMV.Email;
                        newemployee.EmployeeID = focalpersonmv.employeeMV.EmployeeID;
                        newemployee.MonthlySalary = focalpersonmv.employeeMV.MonthlySalary;
                        newemployee.Name = focalpersonmv.employeeMV.Name;
                        newemployee.Photo = "~/Content/Template/img/user/employee.png";
                        newemployee.Address = focalpersonmv.employeeMV.Address;
                        newemployee.UserID = newuser.UserID;
                        
                        DB.tblEmployees.Add(newemployee);
                        DB.SaveChanges();
                        transaction.Commit();
                       
                    
                }
                    return RedirectToAction("AllCompanyBranches","Branch");
                }
            catch (Exception ex)
            {
                transaction.Rollback();
                ModelState.AddModelError("Name", "Must fill all fields with correct data!");
                return View(focalpersonmv);
            }
            }
        }
        
    }
}