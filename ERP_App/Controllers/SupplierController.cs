using ERP.DatabaseLayer;
using ERP_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class SupplierController : Controller
    {
        private BusinessERP_DBEntities DB = new BusinessERP_DBEntities();
        // GET: Customer
        public ActionResult AllBranchSupplier()
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

            var list = new List<SupplierMV>();
            var suppliers = DB.tblSuppliers.ToList();
            if (usertypeid != 1)
            {
                suppliers = DB.tblSuppliers.Where(c => c.BranchID == branchid && c.CompanyID == companyid).ToList();
            }
            
            foreach (var supplier in suppliers)
            {
                var gsupplier = new SupplierMV();

                gsupplier.SupplierID = supplier.SupplierID;
                gsupplier.SupplierName = supplier.SupplierName;
                gsupplier.SupplierConatctNo = supplier.SupplierConatctNo;
                gsupplier.SupplierAddress = supplier.SupplierAddress;
                gsupplier.SupplierEmail = supplier.SupplierEmail;
                gsupplier.Discription = supplier.Discription;
                gsupplier.BranchID = supplier.BranchID;
                gsupplier.CompanyID = supplier.CompanyID;
                gsupplier.UserID = supplier.UserID;
                gsupplier.CreatedBy = supplier.tblUser.UserName;
                list.Add(gsupplier);
            }
            return View(list);
        }

        public ActionResult CreateBranchSupplier()
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

            var supplierMV = new SupplierMV();
            return View(supplierMV);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBranchSupplier(SupplierMV supplierMV)
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
                var checksupplier = DB.tblSuppliers.Where(u => u.BranchID == branchid && u.CompanyID == companyid && u.SupplierName == supplierMV.SupplierName).FirstOrDefault();
                if (checksupplier == null)
                {
                    var newsupplier = new tblSupplier();
                    newsupplier.SupplierName = supplierMV.SupplierName;
                    newsupplier.SupplierConatctNo = supplierMV.SupplierConatctNo;
                    newsupplier.SupplierEmail = supplierMV.SupplierEmail;
                    newsupplier.SupplierAddress = supplierMV.SupplierAddress;
                    newsupplier.Discription = supplierMV.Discription;
                    newsupplier.BranchID = branchid;
                    newsupplier.CompanyID = companyid;
                    newsupplier.UserID = userid;

                    DB.tblSuppliers.Add(newsupplier);
                    DB.SaveChanges();
                    return RedirectToAction("AllBranchSupplier");
                }
                else
                {
                    ModelState.AddModelError("SupplierName", "Already Exists");
                }
            }

            return View(supplierMV);
        }
        public ActionResult EditBranchSupplier(int? id)

        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {



                return RedirectToAction("Login", "Home");

            }

            var userid = 0;
            var usertypeid = 0;
            var companyid = 0;
            var branchid = 0;
            var branchtypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out branchtypeid);
            var supplier = DB.tblSuppliers.Find(id);
            var newsupp = new SupplierMV();
            newsupp.SupplierID = supplier.SupplierID;
            newsupp.SupplierName = supplier.SupplierName;
            newsupp.SupplierAddress = supplier.SupplierAddress;
            newsupp.SupplierConatctNo = supplier.SupplierConatctNo;
            newsupp.SupplierEmail = supplier.SupplierEmail;
            newsupp.Discription = supplier.Discription;
            newsupp.CompanyID = supplier.CompanyID;
            newsupp.BranchID = supplier.BranchID;
            newsupp.UserID = supplier.UserID;

            return View(newsupp);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult EditBranchSupplier(SupplierMV suppliermv)

        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {



                return RedirectToAction("Login", "Home");

            }

            var userid = 0;
            var usertypeid = 0;
            var companyid = 0;
            var branchid = 0;
            var branchtypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out branchtypeid);

            if (ModelState.IsValid)
            {
                var supplier = DB.tblSuppliers.Where(e => e.CompanyID == companyid && e.BranchID == branchid && e.SupplierName == suppliermv.SupplierName && e.SupplierID != suppliermv.SupplierID).FirstOrDefault();
                if (supplier == null)
                {
                    var newsupp = DB.tblSuppliers.Find(suppliermv.SupplierID);


                    newsupp.SupplierName = suppliermv.SupplierName;
                    newsupp.SupplierAddress = suppliermv.SupplierAddress;
                    newsupp.SupplierConatctNo = suppliermv.SupplierConatctNo;
                    newsupp.SupplierEmail = suppliermv.SupplierEmail;
                    newsupp.Discription = suppliermv.Discription;

                    newsupp.UserID = suppliermv.UserID;


                    DB.Entry(newsupp).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllBranchSupplier");
                }
                else
                {
                    ModelState.AddModelError("SupplierName", "already");
                }
            }

            return View(suppliermv);


        }
        public JsonResult GetSelectedSupplierDetails(int? id)
        {
            var data = new Supplier();
            if (id > 0)
            {
                var supplier = DB.tblSuppliers.Find(id);
                data.ContactNo = supplier.SupplierConatctNo;
                data.Address = supplier.SupplierAddress;
            }
            else
            {
                data.ContactNo = "";
                data.Address = "";
            }


            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
    class Supplier
    {
        public string ContactNo { get; set; }
        public string Address { get; set; }
    }
}