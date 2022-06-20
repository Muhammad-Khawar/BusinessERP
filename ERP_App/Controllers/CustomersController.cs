using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP.DatabaseLayer;

namespace ERP_App.Controllers
{
    public class CustomersController : Controller
    {
        private BusinessERP_DBEntities db = new BusinessERP_DBEntities();

        // GET: Customers
        public ActionResult Index()
        {
            var c = (tblCustomer)Session["Customer"];
            
            var customer = db.tblCustomers.Where(x=>x.CustomerID == c.CustomerID).ToList();
            return View(customer);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCustomer tblCustomer = db.tblCustomers.Find(id);
            if (tblCustomer == null)
            {
                return HttpNotFound();
            }
            return View(tblCustomer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.BranchID = new SelectList(db.tblBranches, "BranchID", "BranchName");
            ViewBag.CompanyID = new SelectList(db.tblCompanies, "CompanyID", "Name");
            ViewBag.UserID = new SelectList(db.tblUsers, "UserID", "FullName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,Customername,CustomerContact,CustomerArea,CustomerAddress,Description,BranchID,CompanyID,UserID,CustomerEmail,CustomerPassword")] tblCustomer tblCustomer)
        {
            if (ModelState.IsValid)
            {
                db.tblCustomers.Add(tblCustomer);
                db.SaveChanges();
                //kkkkkkkkkkkkkkkkkkk
                Session["Customer"] = db.tblCustomers.Where(x => x.CustomerID == tblCustomer.CustomerID).FirstOrDefault();
                //kkkkkkkkkkkkkkkkkkkkkk
                return RedirectToAction("Index");
            }

            ViewBag.BranchID = new SelectList(db.tblBranches, "BranchID", "BranchName", tblCustomer.BranchID);
            ViewBag.CompanyID = new SelectList(db.tblCompanies, "CompanyID", "Name", tblCustomer.CompanyID);
            ViewBag.UserID = new SelectList(db.tblUsers, "UserID", "FullName", tblCustomer.UserID);
            return View(tblCustomer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCustomer tblCustomer = db.tblCustomers.Find(id);
            if (tblCustomer == null)
            {
                return HttpNotFound();
            }
            return View(tblCustomer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblCustomer tblCustomer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCustomer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblCustomer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCustomer tblCustomer = db.tblCustomers.Find(id);
            if (tblCustomer == null)
            {
                return HttpNotFound();
            }
            return View(tblCustomer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCustomer tblCustomer = db.tblCustomers.Find(id);
            db.tblCustomers.Remove(tblCustomer);
            db.SaveChanges();
            //Session["Customer"] = null;
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tblCustomer a)
        {
            //Please Count the records from Database Admin when Table email and password matches with our posted email and password
            //If email and password match then result is 1
            //If not match then result is 0
            var  b = db.tblCustomers.Where(x => x.CustomerEmail == a.CustomerEmail && x.CustomerPassword == a.CustomerPassword).FirstOrDefault();
            if (b != null)
            {
                Session["Customer"] = b;
                return RedirectToAction("IndexCustomer", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();
            }

        }
        public ActionResult Logout()
        {
            Session["Customer"] = null;
            return RedirectToAction("IndexCustomer", "Home");
        }
        public ActionResult CustomerHistory()
        {
            return View();
        }
        public ActionResult PrintSaleInvoice(int? id)
        {
            var o = db.tblOrders.Where(O => O.OrderID == id).ToList();
            return View(o);
        }
        public ActionResult OrderCancellation(int id)
        {
            var status = db.tblOrders.Where(x => x.OrderID == id).Select(x => x.OrderStatus).FirstOrDefault();
            if(status =="Paid")
            {
                return RedirectToAction("CustomerHistory");
            }
            else
            {
                var o = db.tblOrders.Where(x => x.OrderID == id && x.OrderStatus == "Cash on Delivery").FirstOrDefault();
                o.Status = "Cancelled";
                db.Entry(o).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CustomerHistory");
            }
        }
        public ActionResult CancelledOrder()
        {
            return View();
        }
        public ActionResult OrderActivate(int id)
        {
            var o = db.tblOrders.Where(x => x.OrderID == id).FirstOrDefault();
            o.Status = "Active";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            //return RedirectToAction("CustomerHistory");
            return RedirectToAction("CustomerHistory");
        }
    }
}
