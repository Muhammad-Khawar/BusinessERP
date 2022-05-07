using ERP.DatabaseLayer;
using ERP_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class CustomerController : Controller
    {
        private BusinessERP_DBEntities DB = new BusinessERP_DBEntities();
        // GET: Customer
        public ActionResult AllBranchCustomer()
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

            var list = new List<CustomerMV>();
            
            var customers = DB.tblCustomers.Where(c => c.BranchID == branchid && c.CompanyID == companyid).ToList();
            foreach (var customer in customers)
            {
                var gcustomer = new CustomerMV();

                gcustomer.CustomerID = customer.CustomerID;
                gcustomer.Customername = customer.Customername;
                gcustomer.CustomerContact = customer.CustomerContact;
                gcustomer.CustomerArea = customer.CustomerArea;
                gcustomer.CustomerAddress = customer.CustomerAddress;
                gcustomer.Description = customer.Description;
                gcustomer.BranchID = customer.BranchID;
                gcustomer.CompanyID = customer.CompanyID;
                gcustomer.UserID = customer.UserID;
                gcustomer.CreatedBy = customer.tblUser.UserName;
                list.Add(gcustomer);
            }
            return View(list);
        }
        public ActionResult CreateBranchCustomer()
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

            var customerMV = new CustomerMV();
            return View(customerMV);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBranchCustomer(CustomerMV customerMV)
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
                var checkcustomer = DB.tblCustomers.Where(u => u.BranchID == branchid && u.CompanyID == companyid && u.Customername == customerMV.Customername).FirstOrDefault();
                if (checkcustomer == null)
                {
                    var newcustomer = new tblCustomer();
                    newcustomer.Customername = customerMV.Customername;
                    newcustomer.CustomerContact = customerMV.CustomerContact;
                    newcustomer.CustomerArea = customerMV.CustomerArea;
                    newcustomer.CustomerAddress = customerMV.CustomerAddress;
                    newcustomer.Description = customerMV.Description;
                    newcustomer.BranchID = branchid;
                    newcustomer.CompanyID = companyid;
                    newcustomer.UserID = userid;

                    DB.tblCustomers.Add(newcustomer);
                    DB.SaveChanges();
                    return RedirectToAction("AllBranchCustomer");
                }
                else
                {
                    ModelState.AddModelError("Customername", "Already Exists");
                }
            }

            return View(customerMV);
        }
    }
}