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
                using (BusinessERP_DBEntities db = new BusinessERP_DBEntities())
                {
                    var user = db.tblUsers.Where(u => u.Email == UserEmail && u.Password == Password && u.IsActive==true).FirstOrDefault();
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
                        Session["IsActive"] = user.IsActive==true ? "Active" : "No-Active";
                        var usertypeid = user.UserTypeID;

                        if (user.UserTypeID > 2) // Means k woh jis b branch ka ho. es liye greater than 2 hai.
                        {
                            var employee = db.tblEmployees.Where(u => u.UserID == user.UserID).FirstOrDefault(); //user kis company ka hai.
                            if(employee == null)
                            {
                                ViewBag.ErrorMsg = "Email or Passsword is incorrect";
                                Session["UserID"] = string.Empty;
                                Session["UserName"] = string.Empty;
                                Session["Email"] = string.Empty;
                                Session["UserTypeId"] = string.Empty;
                                Session["UserType"] = string.Empty;
                                Session["IsActive"] = string.Empty;
                                //Employee
                                Session["EmployeeID"] = string.Empty;
                                Session["Name"] = string.Empty;
                                Session["ContactNo"] = string.Empty;
                                Session["Photo"] = string.Empty;
                                Session["Email"] = string.Empty;
                                Session["Address"] = string.Empty;
                                Session["CNIC"] = string.Empty;
                                Session["Designation"] = string.Empty;
                                Session["Description"] = string.Empty;
                                Session["MonthlySalary"] = string.Empty;
                                Session["BranchID"] = string.Empty;
                                Session["CompanyID"] = string.Empty;
                                Session["EmployeeUserID"] = string.Empty;
                                Session["BranchTypeID"] = string.Empty;

                                //Company
                                Session["CompanyID"] = string.Empty;
                                Session["Name"] = string.Empty;
                                Session["Logo"] = string.Empty;
                                return View();
                            }
                            else
                            {
                                //Login user ka full detail chahiye.
                                Session["EmployeeID"] = employee.EmployeeID;
                                Session["Name"] = employee.Name;
                                Session["ContactNo"] = employee.ContactNo;
                                Session["Photo"] = employee.Photo;
                                Session["Email"] = employee.Email;
                                Session["Address"] = employee.Address;
                                Session["CNIC"] = employee.CNIC;
                                Session["Designation"] = employee.Designation;
                                Session["Description"] = employee.Description;
                                Session["MonthlySalary"] = employee.MonthlySalary;
                                Session["BranchID"] = employee.BranchID;
                                Session["CompanyID"] = employee.CompanyID;
                                Session["EmployeeUserID"] = employee.UserID;
                                Session["BranchTypeID"] = db.tblBranches.Find(employee.BranchID).BranchTypeID;
                            }
                            var company = db.tblCompanies.Find(employee.CompanyID); // Woh employee kis company ka hai.
                            if (company == null)
                            {
                                ViewBag.ErrorMsg = "Email or Passsword is incorrect";
                                Session["UserID"] = string.Empty;
                                Session["UserName"] = string.Empty;
                                Session["Email"] = string.Empty;
                                Session["UserTypeId"] = string.Empty;
                                Session["UserType"] = string.Empty;
                                Session["IsActive"] = string.Empty;
                                //Employee
                                Session["EmployeeID"] = string.Empty;
                                Session["Name"] = string.Empty;
                                Session["ContactNo"] = string.Empty;
                                Session["Photo"] = string.Empty;
                                Session["Email"] = string.Empty;
                                Session["Address"] = string.Empty;
                                Session["CNIC"] = string.Empty;
                                Session["Designation"] = string.Empty;
                                Session["Description"] = string.Empty;
                                Session["MonthlySalary"] = string.Empty;
                                Session["BranchID"] = string.Empty;
                                Session["CompanyID"] = string.Empty;
                                Session["EmployeeUserID"] = string.Empty;
                                Session["BranchTypeID"] = string.Empty;

                                //Company
                                Session["CompanyID"] = string.Empty;
                                Session["Name"] = string.Empty;
                                Session["Logo"] = string.Empty;
                                return View();
                            }
                            else
                            {
                                Session["CompanyID"] = company.CompanyID;
                                Session["Name"] = company.Name;
                                Session["Logo"] = company.Logo;
                            }

                        }
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

            //Employee
            Session["EmployeeID"] = string.Empty;
            Session["Name"] = string.Empty;
            Session["ContactNo"] = string.Empty;
            Session["Photo"] = string.Empty;
            Session["Email"] = string.Empty;
            Session["Address"] = string.Empty;
            Session["CNIC"] = string.Empty;
            Session["Designation"] = string.Empty;
            Session["Description"] = string.Empty;
            Session["MonthlySalary"] = string.Empty;
            Session["BranchID"] = string.Empty;
            Session["CompanyID"] = string.Empty;
            Session["EmployeeUserID"] = string.Empty;
            Session["BranchTypeID"] = string.Empty;

            //Company
            Session["CompanyID"] = string.Empty;
            Session["Name"] = string.Empty;
            Session["Logo"] = string.Empty;

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
            //Employee
            Session["EmployeeID"] = string.Empty;
            Session["Name"] = string.Empty;
            Session["ContactNo"] = string.Empty;
            Session["Photo"] = string.Empty;
            Session["Email"] = string.Empty;
            Session["Address"] = string.Empty;
            Session["CNIC"] = string.Empty;
            Session["Designation"] = string.Empty;
            Session["Description"] = string.Empty;
            Session["MonthlySalary"] = string.Empty;
            Session["BranchID"] = string.Empty;
            Session["CompanyID"] = string.Empty;
            Session["EmployeeUserID"] = string.Empty;
            Session["BranchTypeID"] = string.Empty;

            //Company
            Session["CompanyID"] = string.Empty;
            Session["Name"] = string.Empty;
            Session["Logo"] = string.Empty;
            return RedirectToAction("Login");
        }
    }
}