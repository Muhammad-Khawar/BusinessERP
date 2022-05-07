using ERP.DatabaseLayer;
using ERP_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class StockController : Controller
    {
        private BusinessERP_DBEntities DB = new BusinessERP_DBEntities();
        // GET: Stock
        public ActionResult AllCategories()
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

     
            var list = new List<CategoryMV>();
            var categories = DB.tblCategories.Where(c => c.CompanyID == companyid && c.BranchID == branchid).ToList();
            foreach (var category in categories)
            {
                var username = category.tblUser.UserName;
                list.Add(new CategoryMV { 
                    BranchID = category.BranchID, 
                    CategoryID = category.CategoryID, 
                    categoryName =category.categoryName, 
                    CompanyID = category.CompanyID, 
                    UserID = category.UserID,
                    CreatedBy = username
                });
            }
            return View(list);
        }
        public ActionResult CreateCategory()
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


            var categoryMV = new CategoryMV();
            return View(categoryMV);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CategoryMV categorymv)
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
                var checkcategory = DB.tblCategories.Where(u => u.categoryName == categorymv.categoryName.Trim() && u.CompanyID ==companyid && u.BranchID == branchid).FirstOrDefault();
                if (checkcategory == null)
                {
                    var newcategory = new tblCategory();
                    newcategory.CompanyID = companyid;
                    newcategory.BranchID = branchid;
                    newcategory.categoryName = categorymv.categoryName;
                    newcategory.UserID = userid;
                    DB.tblCategories.Add(newcategory);
                    DB.SaveChanges();
                    return RedirectToAction("AllCategories");
                }
                else
                {
                    ModelState.AddModelError("categoryName", "Already Exists");
                }
            }

            return View(categorymv);
        }

        public ActionResult EditCategory(int? categoryID)
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

            var category = DB.tblCategories.Find(categoryID);
            var categoryMV = new CategoryMV();
            categoryMV.CategoryID = category.CategoryID;
            categoryMV.BranchID = category.BranchID;
            categoryMV.CompanyID = category.CompanyID;
            categoryMV.categoryName = category.categoryName;
            return View(categoryMV);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(CategoryMV categorymv)
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
                var checkcategory = DB.tblCategories.Where(u => u.categoryName == categorymv.categoryName.Trim() &&u.CategoryID!= categorymv.CategoryID && u.CompanyID == companyid && u.BranchID == branchid).FirstOrDefault();
                if (checkcategory == null)
                {

                    var editcategory = DB.tblCategories.Find(categorymv.CategoryID);
                    editcategory.categoryName = categorymv.categoryName;
                    editcategory.UserID = userid;
                    DB.Entry(editcategory).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllCategories");
                }
                else
                {
                    ModelState.AddModelError("categoryName", "Already Exists");
                }
            }

            return View(categorymv);
        }
        public ActionResult StockProducts()
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

            var stock = DB.tblStocks.Where(s => s.CompanyID == companyid && s.BranchID == branchid).ToList();
            var list = new List<StockMV>();
            foreach (var product in stock)
            {
                var item = new StockMV();
                item.BranchID = product.BranchID;
                item.CategoryID = product.CategoryID;
                item.CompanyID = product.CompanyID;
                item.CreatedBy = product.tblUser.UserName;
                item.CurrentPurchaseUnitPrice = product.CurrentPurchaseUnitPrice;
                item.Description = product.Description;
                item.Manufacture = product.Manufacture;
                item.ExpiryDate = product.ExpiryDate;
                item.IsActive = product.IsActive;
                item.ProductID = product.ProductID;
                item.ProductName = product.ProductName;
                item.Quantity = product.Quantity;
                item.SaleUnitPrice = product.SaleUnitPrice;
                item.StockTreshHoldQuantity = product.StockTreshHoldQuantity;
                item.UserID = product.UserID;
                item.CategoryName = product.tblCategory.categoryName;
                list.Add(item);
            }

            return View(list);
        }
        public ActionResult CreateProduct()
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

            var stockMV = new StockMV();
            ViewBag.CategoryID = new SelectList(DB.tblCategories.Where(c => c.CompanyID == companyid && c.BranchID == branchid), "CategoryID", "categoryName","0");
            return View(stockMV);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(StockMV stockMV)
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
                var checkproduct = DB.tblStocks.Where(u => u.ProductName == stockMV.ProductName.Trim() && u.CategoryID == stockMV.CategoryID && u.CompanyID == companyid && u.BranchID == branchid).FirstOrDefault();
                if (checkproduct == null)
                {
                    var newproduct = new tblStock();
                    newproduct.CategoryID = stockMV.CategoryID;
                    newproduct.CompanyID = companyid;
                    newproduct.BranchID = branchid;
                    newproduct.ProductName = stockMV.ProductName;
                    newproduct.Quantity = stockMV.Quantity;
                    newproduct.SaleUnitPrice = stockMV.SaleUnitPrice;
                    newproduct.CurrentPurchaseUnitPrice = stockMV.CurrentPurchaseUnitPrice;
                    newproduct.ExpiryDate = stockMV.ExpiryDate;
                    newproduct.Manufacture = stockMV.Manufacture;
                    newproduct.StockTreshHoldQuantity = stockMV.StockTreshHoldQuantity;
                    newproduct.Description = stockMV.Description;
                    newproduct.UserID =userid;
                    newproduct.IsActive = stockMV.IsActive;
                    DB.tblStocks.Add(newproduct);
                    DB.SaveChanges();
                    return RedirectToAction("StockProducts");
                }
                else
                {
                    ModelState.AddModelError("ProductName", "Already Exists");
                }
            }
            ViewBag.CategoryID = new SelectList(DB.tblCategories.Where(c => c.CompanyID == companyid && c.BranchID == branchid), "CategoryID", "categoryName", stockMV.CategoryID);
            return View(stockMV);
        }

        public ActionResult EditProduct(int? productID)
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

            var product = DB.tblStocks.Find(productID);
            var stockMV = new StockMV();
            stockMV.BranchID = product.BranchID;
            stockMV.CompanyID = product.CompanyID;
            stockMV.CurrentPurchaseUnitPrice = product.CurrentPurchaseUnitPrice;
            stockMV.Description = product.Description;
            stockMV.ExpiryDate = product.ExpiryDate;
            stockMV.Manufacture = product.Manufacture;
            stockMV.ProductID = product.ProductID;
            stockMV.ProductName = product.ProductName;
            stockMV.SaleUnitPrice = product.SaleUnitPrice;
            stockMV.StockTreshHoldQuantity = product.StockTreshHoldQuantity;
            stockMV.IsActive = product.IsActive;
            ViewBag.CategoryID = new SelectList(DB.tblCategories.Where(c => c.CompanyID == companyid && c.BranchID == branchid), "CategoryID", "categoryName",product.CategoryID);
            return View(stockMV);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(StockMV stockMV)
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
                var checkproduct = DB.tblStocks.Where(u => u.ProductName == stockMV.ProductName.Trim() && u.CategoryID == stockMV.CategoryID && u.CompanyID == companyid && u.BranchID == branchid && u.ProductID != stockMV.ProductID).FirstOrDefault();
                if (checkproduct == null)
                {
                    
                    var editproduct = DB.tblStocks.Find(stockMV.ProductID);
                 
                    editproduct.ProductName = stockMV.ProductName;  
                    editproduct.SaleUnitPrice = stockMV.SaleUnitPrice;
                    editproduct.CurrentPurchaseUnitPrice = stockMV.CurrentPurchaseUnitPrice;
                    editproduct.ExpiryDate = stockMV.ExpiryDate;
                    editproduct.Manufacture = stockMV.Manufacture;
                    editproduct.StockTreshHoldQuantity = stockMV.StockTreshHoldQuantity;
                    editproduct.Description = stockMV.Description;
                    editproduct.UserID = userid;
                    editproduct.IsActive = stockMV.IsActive;
                    DB.Entry(editproduct).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("StockProducts");
                }
                else
                {
                    ModelState.AddModelError("ProductName", "Already Exists");
                }
            }
            ViewBag.CategoryID = new SelectList(DB.tblCategories.Where(c => c.CompanyID == companyid && c.BranchID == branchid), "CategoryID", "categoryName", stockMV.CategoryID);
            return View(stockMV);
        }
    }
}