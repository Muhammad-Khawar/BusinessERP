using ERP.DatabaseLayer;
using ERP_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class PurchaseController : Controller
    {
        private BusinessERP_DBEntities DB = new BusinessERP_DBEntities();
        // GET: Purchase
        public ActionResult PurchaseStockProducts()
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

            var stock = DB.tblPurchaseCartDetails.Where(s => s.CompanyID == companyid && s.BranchID == branchid && s.UserID == userid).ToList();

            var purchaseitems = new PurchaseCartMV();
            var list = new List<PurchaseItemsMV>();

            double subTotal = 0;
            foreach (var product in stock)
            {
                var item = new PurchaseItemsMV();
                //item.BranchID = product.BranchID;
                //item.CategoryID = product.CategoryID;
                //item.CompanyID = product.CompanyID;
                //item.CreatedBy = product.tblUser.UserName;
                item.PreviousPurchaseUnitPrice = product.PrevoiusPurchaseUnitPrice;
                item.CurrentPurchaseUnitPrice = product.purchaseUnitPrice;
                item.Description = product.Description;
                item.Manufacture = product.ManufactureDate;
                item.ExpiryDate = product.ExpireDate;
                //item.IsActive = product.IsActive;
                item.ProductID = product.ProductID;

                var getproduct = DB.tblStocks.Find(product.ProductID);
                item.ProductName = getproduct != null ? getproduct.ProductName : "?";

                //item.ProductName = product.tblStock.ProductName;
                item.Quantity = product.PurchaseQuantity;
                item.SaleUnitPrice = (double)product.SaleUnitPrice;
                //item.StockTreshHoldQuantity = product.StockTreshHoldQuantity;
                item.UserID = product.UserID;
                item.CategoryName = product.tblStock.tblCategory.categoryName;
                item.PurchaseCartDetailID = product.PurchaseCartDetailID;
                list.Add(item);

                subTotal = subTotal + ((double)product.purchaseUnitPrice * product.PurchaseQuantity);
            }
            purchaseitems.PurchaseItemList = list;
            purchaseitems.OrderSummary = new PurchaseCartSummaryMV() {  SubTotal = subTotal};
            ViewBag.ProductID = new SelectList(DB.tblStocks.Where(s=>s.BranchID == branchid && s.CompanyID == companyid).ToList(), "ProductID", "ProductName","0");
            if(Session["SupplierID"] == null)
            {
                ViewBag.SupplierID = new SelectList(DB.tblSuppliers.Where(s => s.BranchID == branchid && s.CompanyID == companyid).ToList(), "SupplierID", "SupplierName", "0");
            }
            else
            {
                ViewBag.SupplierID = Session["SupplierID"];
            }
            return View(purchaseitems);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PurchaseStockProducts(PurchaseCartMV purchaseCartMV)
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

            if(ModelState.IsValid)
            {
                var checkproductincart = DB.tblPurchaseCartDetails.Where(i => i.ProductID == purchaseCartMV.ProductID && i.BranchID == branchid && i.CompanyID == companyid &&i.UserID ==userid).FirstOrDefault();
                if(checkproductincart == null)
                {
                    var item = new tblPurchaseCartDetail();
                    item.BranchID = branchid;
                    item.CompanyID = companyid;
                    item.ProductID = purchaseCartMV.ProductID;
                    item.PurchaseQuantity = purchaseCartMV.PurchaseQuantity;
                    item.purchaseUnitPrice = purchaseCartMV.CurrentPurchaseUnitPrice;
                    item.SaleUnitPrice = purchaseCartMV.SaleUnitPrice;
                    item.PrevoiusPurchaseUnitPrice = purchaseCartMV.PrevoiusPurchaseUnitPrice;
                    item.ManufactureDate = purchaseCartMV.ManufactureDate;
                    item.ExpireDate = purchaseCartMV.ExpireDate;
                    item.Description = purchaseCartMV.Description;

                    item.UserID = userid;
                    DB.tblPurchaseCartDetails.Add(item);
                    DB.SaveChanges();
                    //return RedirectToAction("PurchaseStockProducts");

                    purchaseCartMV.ProductID = 0;
                    purchaseCartMV.PurchaseQuantity = 0;
                    purchaseCartMV.CurrentPurchaseUnitPrice = 0;
                    purchaseCartMV.SaleUnitPrice = 0;
                    purchaseCartMV.PrevoiusPurchaseUnitPrice = 0;
                    purchaseCartMV.ManufactureDate = DateTime.Now;
                    purchaseCartMV.ExpireDate = DateTime.Now;
                    purchaseCartMV.Description = string.Empty;
                }
                else
                {
                    ModelState.AddModelError("ProductID", "Already in Purchase Cart:");
                }
            }

            var stock = DB.tblPurchaseCartDetails.Where(s => s.CompanyID == companyid && s.BranchID == branchid && s.UserID == userid).ToList();

            var purchaseitems = new PurchaseCartMV();
            var list = new List<PurchaseItemsMV>();
            foreach (var product in stock)
            {
                var item = new PurchaseItemsMV();
                //item.BranchID = product.BranchID;
                //item.CategoryID = product.CategoryID;
                //item.CompanyID = product.CompanyID;
                //item.CreatedBy = product.tblUser.UserName;
                item.PreviousPurchaseUnitPrice = product.PrevoiusPurchaseUnitPrice;
                item.CurrentPurchaseUnitPrice = product.purchaseUnitPrice;
                item.Description = product.Description;
                item.Manufacture = product.ManufactureDate;
                item.ExpiryDate = product.ExpireDate;
                //item.IsActive = product.IsActive;
                item.ProductID = product.ProductID;
                
                var getproduct = DB.tblStocks.Find(product.ProductID);
                item.ProductName = getproduct != null ? getproduct.ProductName : "?";

                item.Quantity = product.PurchaseQuantity;
                item.SaleUnitPrice = (double)product.SaleUnitPrice;
                //item.StockTreshHoldQuantity = product.StockTreshHoldQuantity;
                item.PurchaseCartDetailID = product.PurchaseCartDetailID;
                item.UserID = product.UserID;
                item.CategoryName = product.tblStock.tblCategory.categoryName;
                
                list.Add(item);
            }
            purchaseCartMV.PurchaseItemList = list;
            
            ViewBag.ProductID = new SelectList(DB.tblStocks.Where(s => s.BranchID == branchid && s.CompanyID == companyid).ToList(), "ProductID", "ProductName", purchaseCartMV.ProductID);
            ViewBag.SupplierID = new SelectList(DB.tblSuppliers.Where(s => s.BranchID == branchid && s.CompanyID == companyid).ToList(), "SupplierID", "SupplierName", purchaseCartMV.SupplierID);
            Session["SupplierID"] = ViewBag.SupplierID;
            //return View(purchaseCartMV);
            return RedirectToAction("PurchaseStockProducts");
        }
        public ActionResult DeletePurchaseItem(int ? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var pitem = DB.tblPurchaseCartDetails.Find(id);
            DB.Entry(pitem).State = System.Data.Entity.EntityState.Deleted;
            DB.SaveChanges();
            return RedirectToAction("PurchaseStockProducts");
        }
        public ActionResult CheckoutPurchase(int supplierid, bool ispaymentispaid,float ? estimatedtax, float? shippingfee, float? subtotal)
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

            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var datetime = DateTime.Now;
                    var supplier = DB.tblSuppliers.Find(supplierid);
                    if(supplier == null)
                    {
                        ModelState.AddModelError("SupplierID", "Please Select the Supplier!");
                        transaction.Rollback();
                    }
                    //kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk
                    //3. save in DB
                    var order = new tblOrder();
                    order.OrderDate = datetime;
                    order.OrderStatus = "Paid";
                    order.OrderType = "Purchase";
                    order.OrderName = supplier.SupplierName;
                    order.OrderEmail = supplier.SupplierEmail;
                    order.OrderContact = supplier.SupplierConatctNo;
                    order.OrderAddress = supplier.SupplierAddress;
                    DB.tblOrders.Add(order);
                    DB.SaveChanges();
                    //kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk

                    float totalamount = (float)subtotal + (float)estimatedtax + (float)shippingfee;
                    string invoiceno = "PUR" + datetime.ToString("yyyymmddhhmmss") + userid;
                    var invoiceheader = new tblSupplierInvoice();

                    invoiceheader.SupplierInvoiceID = (int)supplierid;
                    invoiceheader.SupplierID = supplierid;
                    invoiceheader.CompanyID = companyid;
                    invoiceheader.BranchID = branchid;
                    invoiceheader.InvoiceNo = invoiceno;
                    invoiceheader.TotalAmount = totalamount;
                    invoiceheader.InvoiceDate = datetime;
                    invoiceheader.Description = "";
                    invoiceheader.UserID = userid;
                    invoiceheader.SubTotalAmount = subtotal;
                    invoiceheader.EstimatedTax = (float)estimatedtax;
                    invoiceheader.ShippingFee = (float)shippingfee;
                    
                    DB.tblSupplierInvoices.Add(invoiceheader);
                    DB.SaveChanges();


                    var purchasestock = DB.tblPurchaseCartDetails.Where(s => s.CompanyID == companyid && s.BranchID == branchid && s.UserID == userid).ToList();
                    foreach (var product in purchasestock)
                    {
                        var purchaseitem = new tblSupplierInvoiceDetail();

                        purchaseitem.SupplierInvoiceID = invoiceheader.SupplierInvoiceID;
                        purchaseitem.ProductID = product.ProductID;
                        purchaseitem.PurchaseQuantity = product.PurchaseQuantity;
                        purchaseitem.purchaseUnitPrice = product.purchaseUnitPrice;
                        purchaseitem.PreviousPurchaseUnitPrice = product.PrevoiusPurchaseUnitPrice;
                        purchaseitem.ManufactureDate = (DateTime)product.ManufactureDate;
                        purchaseitem.ExpiryDate = (DateTime)product.ExpireDate;

                        DB.tblSupplierInvoiceDetails.Add(purchaseitem);
                        DB.SaveChanges();
                        //kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk
                        var od = new tblOrderDetail();
                        int orderID = DB.tblOrders.Max(x => x.OrderID);
                        od.OrderFID = orderID;
                        od.ProductFID = product.ProductID;
                        od.Quantity = product.PurchaseQuantity;
                        od.PurchasePrice = (decimal)product.purchaseUnitPrice;
                        od.SalePrice = (decimal)product.SaleUnitPrice;
                        od.CompanyFID = product.CompanyID;
                        od.BranchFID = product.BranchID;
                        DB.tblOrderDetails.Add(od);
                        DB.SaveChanges();
                        //kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk
                        var stockproduct = DB.tblStocks.Find(product.ProductID);
                        stockproduct.Manufacture = (DateTime)product.ManufactureDate;
                        stockproduct.ExpiryDate = (DateTime)product.ExpireDate;
                        stockproduct.Quantity = stockproduct.Quantity + product.PurchaseQuantity;
                        stockproduct.CurrentPurchaseUnitPrice = product.purchaseUnitPrice;
                        stockproduct.SaleUnitPrice = (double)product.SaleUnitPrice;
                        DB.Entry(stockproduct).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                    }

                    // Purchase Product Debit Transaction 
                    int FinancialYearID = 0;
                    var financial = DB.tblFinancialYears.Where(s=>s.IsActive == true).FirstOrDefault();
                    if(financial == null)
                    {
                        ModelState.AddModelError("ProductID", "Financial Year is not set!");
                        transaction.Rollback();
                    }
                    FinancialYearID = financial.FinancialYearID;
                    int AccountHeadID = 0;
                    int AccountControlID = 0;
                    int AccountSubControlID = 0;

                    var debitentry = DB.tblAccountSettings.Where(s => s.AccountActivityID == 1 && s.CompanyID == companyid && s.BranchID == branchid).FirstOrDefault();
                    if (debitentry == null)
                    {
                        ModelState.AddModelError("ProductID", "First Set Account Flow!");
                        transaction.Rollback();
                    }

                    AccountHeadID = debitentry.AccountHeadID;
                    AccountControlID = debitentry.AccountControlID;
                    AccountSubControlID = debitentry.AccountSubControlID;

                    var setdebitentry = new tblTransaction();
                    setdebitentry.FinancialYearID = FinancialYearID;
                    setdebitentry.AccountHeadID = AccountHeadID;
                    setdebitentry.AccountControlID = AccountControlID;
                    setdebitentry.AccountSubControlID =AccountSubControlID;
                    setdebitentry.InvoiceNo = invoiceno;
                    setdebitentry.CompanyID = companyid;
                    setdebitentry.BranchID = branchid;
                    setdebitentry.Credit = 0;
                    setdebitentry.Debit = totalamount;
                    setdebitentry.TransectionDate = datetime;
                    setdebitentry.TransectionTitle ="Purchase From " + supplier.SupplierName;
                    setdebitentry.UserID =userid;
                    
                    DB.tblTransactions.Add(setdebitentry);
                    DB.SaveChanges();

                    //Credit Entry: Payment Pending activity
                    var creditentry = DB.tblAccountSettings.Where(s => s.AccountActivityID == 5 && s.CompanyID == companyid && s.BranchID == branchid).FirstOrDefault();
                    if (creditentry == null)
                    {
                        ModelState.AddModelError("ProductID", "First Set Account Flow!");
                        transaction.Rollback();
                    }

                    AccountHeadID = creditentry.AccountHeadID;
                    AccountControlID = creditentry.AccountControlID;
                    AccountSubControlID = creditentry.AccountSubControlID;

                    var setcreditentry = new tblTransaction();
                    setcreditentry.FinancialYearID = FinancialYearID;
                    setcreditentry.AccountHeadID = AccountHeadID;
                    setcreditentry.AccountControlID = AccountControlID;
                    setcreditentry.AccountSubControlID = AccountSubControlID;
                    setcreditentry.InvoiceNo = invoiceno;
                    setcreditentry.CompanyID = companyid;
                    setcreditentry.BranchID = branchid;
                    setcreditentry.Credit = totalamount;
                    setcreditentry.Debit = 0;
                    setcreditentry.TransectionDate = datetime;
                    setcreditentry.TransectionTitle = "Purchase Payment is Pending( " + supplier.SupplierName +")";
                    setcreditentry.UserID = userid;

                    DB.tblTransactions.Add(setcreditentry);
                    DB.SaveChanges();

                    if (ispaymentispaid == true)
                    {
                        invoiceno = "PPP" + DateTime.Now.ToString("yyyymmddhhmmss") + userid; //Payment Purchase Paid
                        var invoicepayment = new tblSupplierPayment();

                        invoicepayment.SupplierID = (int)supplierid;
                        invoicepayment.SupplierInvoiceID = invoiceheader.SupplierInvoiceID;
                        invoicepayment.CompanyID = companyid;
                        invoicepayment.BranchID = branchid;
                        invoicepayment.InvoiceNo = invoiceno;
                        invoicepayment.TotalAmount = totalamount;
                        invoicepayment.PaymentAmount = totalamount;
                        invoicepayment.RemainingBalance = 0;
                        invoicepayment.UserID = userid;
                        invoicepayment.InvoiceDate = DateTime.Now;
                        
                        DB.tblSupplierPayments.Add(invoicepayment);
                        DB.SaveChanges();

                        //Payment Debit Transaction : Purchase Payment Pending activity : 5
                        debitentry = DB.tblAccountSettings.Where(s => s.AccountActivityID == 5 && s.CompanyID == companyid && s.BranchID == branchid).FirstOrDefault();
                        if (debitentry == null)
                        {
                            ModelState.AddModelError("ProductID", "First Set Account Flow!");
                            transaction.Rollback();
                        }

                        AccountHeadID = debitentry.AccountHeadID;
                        AccountControlID = debitentry.AccountControlID;
                        AccountSubControlID = debitentry.AccountSubControlID;

                        setdebitentry = new tblTransaction();
                        setdebitentry.FinancialYearID = FinancialYearID;
                        setdebitentry.AccountHeadID = AccountHeadID;
                        setdebitentry.AccountControlID = AccountControlID;
                        setdebitentry.AccountSubControlID = AccountSubControlID;
                        setdebitentry.InvoiceNo = invoiceno;
                        setdebitentry.CompanyID = companyid;
                        setdebitentry.BranchID = branchid;
                        setdebitentry.Credit = 0;
                        setdebitentry.Debit = totalamount;
                        setdebitentry.TransectionDate = datetime;
                        setdebitentry.TransectionTitle = "Purchase Payment is Transfer (" + supplier.SupplierName+")";
                        setdebitentry.UserID = userid;

                        DB.tblTransactions.Add(setdebitentry);
                        DB.SaveChanges();


                        //Payment Credit Entry: Purchase Payment Paid :6

                        creditentry = DB.tblAccountSettings.Where(s => s.AccountActivityID == 6 && s.CompanyID == companyid && s.BranchID == branchid).FirstOrDefault();
                        if (creditentry == null)
                        {
                            ModelState.AddModelError("ProductID", "First Set Account Flow!");
                            transaction.Rollback();
                        }

                        AccountHeadID = creditentry.AccountHeadID;
                        AccountControlID = creditentry.AccountControlID;
                        AccountSubControlID = creditentry.AccountSubControlID;

                        setcreditentry = new tblTransaction();
                        setcreditentry.FinancialYearID = FinancialYearID;
                        setcreditentry.AccountHeadID = AccountHeadID;
                        setcreditentry.AccountControlID = AccountControlID;
                        setcreditentry.AccountSubControlID = AccountSubControlID;
                        setcreditentry.InvoiceNo = invoiceno;
                        setcreditentry.CompanyID = companyid;
                        setcreditentry.BranchID = branchid;
                        setcreditentry.Credit = totalamount;
                        setcreditentry.Debit = 0;
                        setcreditentry.TransectionDate = datetime;
                        setcreditentry.TransectionTitle = "Purchase Payment is Paid ( " + supplier.SupplierName + ")";
                        setcreditentry.UserID = userid;

                        DB.tblTransactions.Add(setcreditentry);
                        DB.SaveChanges();


                    }
                    DB.Database.ExecuteSqlCommand("TRUNCATE TABLE tblPurchaseCartDetail");

                    transaction.Commit();
                    return RedirectToAction("PrintPurchaseInvoice",new { supplierinvoiceid = invoiceheader.SupplierInvoiceID });
                    
                }
                catch (Exception)
                {

                    transaction.Rollback();
                }
            }

            return View();
        }
        public ActionResult PrintPurchaseInvoice(int? supplierinvoiceid)
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

            var supplierinvoice = DB.tblSupplierInvoices.Find(supplierinvoiceid);

            var purchaseinvoice = new PrintPurchaseInvoiceMV();

            // Set Purcahse Inovice Header Details
            var invoiceheader = new SupplierInvoiceMV();
            invoiceheader.SupplierInvoiceID = supplierinvoice.SupplierInvoiceID;
            invoiceheader.SupplierID = supplierinvoice.SupplierID;
            invoiceheader.CompanyID = supplierinvoice.CompanyID;
            invoiceheader.BranchID = supplierinvoice.BranchID;
            invoiceheader.InvoiceNo = supplierinvoice.InvoiceNo;
            invoiceheader.TotalAmount = supplierinvoice.TotalAmount;
            invoiceheader.InvoiceDate = supplierinvoice.InvoiceDate;
            invoiceheader.Description = supplierinvoice.Description;
            invoiceheader.UserID = supplierinvoice.UserID;
            invoiceheader.SubTotalAmount = supplierinvoice.SubTotalAmount;
            invoiceheader.EstimatedTax = supplierinvoice.EstimatedTax;
            invoiceheader.ShippingFee = supplierinvoice.ShippingFee;
            purchaseinvoice.InvoiceHeader = invoiceheader;

            //Set Purchase Invoice Branch
            var branch = new BranchMV();

            branch.BranchName = supplierinvoice.tblBranch.BranchName;
            branch.BranchContact = supplierinvoice.tblBranch.BranchContact;
            branch.BranchAddress = supplierinvoice.tblBranch.BranchAddress;
            purchaseinvoice.branch = branch;

            //Set Purchase Invoice Supplier
            var supplier = new SupplierMV();
            supplier.SupplierName = supplierinvoice.tblSupplier.SupplierName;
            supplier.SupplierConatctNo = supplierinvoice.tblSupplier.SupplierConatctNo;
            supplier.SupplierAddress = supplierinvoice.tblSupplier.SupplierAddress;
            supplier.SupplierEmail = supplierinvoice.tblSupplier.SupplierEmail;
            purchaseinvoice.supplier = supplier;

            var purchaseitems = new List<SupplierInvoiceDetailMV>();
            foreach (var item in supplierinvoice.tblSupplierInvoiceDetails)
            {
                var product = new SupplierInvoiceDetailMV();
                product.SupplierInvoiceDetailID = item.SupplierInvoiceDetailID;
                product.SupplierInvoiceID = item.SupplierInvoiceID;
                product.ProductID = item.ProductID;
                product.ProductName = item.tblStock.ProductName;
                product.PurchaseQuantity = item.PurchaseQuantity;
                product.purchaseUnitPrice = item.purchaseUnitPrice;
                product.PreviousPurchaseUnitPrice = item.PreviousPurchaseUnitPrice;
                product.ManufactureDate = item.ManufactureDate;
                product.ExpiryDate = item.ExpiryDate;
                product.ItemCost = (item.PurchaseQuantity * item.purchaseUnitPrice);
                purchaseitems.Add(product);
            }
            purchaseinvoice.InvoiceDetails = purchaseitems;
            return View(purchaseinvoice);
        }
        public ActionResult AllPurchases(FilterModel fm)
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

            if (fm.DateFrom == null)
            {
                ViewBag.DateFrom = System.DateTime.Today.ToString("s");
                fm.DateFrom = System.DateTime.Today;
            }
            else
            {
                ViewBag.DateFrom = Convert.ToDateTime(fm.DateFrom).ToString("s");
            }

            if (fm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Now.ToString("s");
                fm.DateTo = System.DateTime.Now;
            }
            else
            {
                ViewBag.DateTo = Convert.ToDateTime(fm.DateTo).ToString("s");
            }

            ViewBag.Category = DB.tblCategories.Where(x => x.CompanyID == companyid && x.BranchID == branchid).Select(x => new SelectListItem { Value = x.CategoryID.ToString(), Text = x.categoryName }).ToList();

            if (fm.Category == null)
            {
                ViewBag.Product = DB.tblStocks.Where(x => x.CompanyID == companyid && x.BranchID == branchid).Select(x => new SelectListItem { Value = x.ProductID.ToString(), Text = x.ProductName + " (" + x.Description + ")" }).ToList();
            }
            else
            {
                ViewBag.Product = DB.tblStocks.Where(x => x.CategoryID == fm.Category && x.CompanyID == companyid && x.BranchID == branchid).Select(x => new SelectListItem { Value = x.ProductID.ToString(), Text = x.ProductName + " (" + x.Description + ")" }).ToList();
            }
            var od = DB.tblSupplierInvoiceDetails.Select(x => x.SupplierInvoiceID).ToList();

            if (fm.Category != null)
            {
                var p = DB.tblStocks.Where(x => x.CategoryID == fm.Category && x.CompanyID == companyid && x.BranchID == branchid).Select(x => x.ProductID).ToList();
                if (fm.Product != null)
                {
                    p = DB.tblStocks.Where(x => x.ProductID == fm.Product && x.CompanyID == companyid && x.BranchID == branchid).Select(x => x.ProductID).ToList();
                }
                od = DB.tblSupplierInvoiceDetails.Where(x => p.Contains(x.ProductID)).Select(x => x.SupplierInvoiceID).ToList();
            }

            var purchaselist = new List<PrintPurchaseInvoiceMV>();

            var allpurchases = DB.tblSupplierInvoices.Where(x=>x.CompanyID == companyid && x.BranchID == branchid && x.InvoiceDate >= fm.DateFrom && x.InvoiceDate <= fm.DateTo && od.Contains(x.SupplierInvoiceID));

            foreach (var supplierinvoice in allpurchases)
            {

           
            var purchaseinvoice = new PrintPurchaseInvoiceMV();

            // Set Purcahse Inovice Header Details
            var invoiceheader = new SupplierInvoiceMV();
            invoiceheader.SupplierInvoiceID = supplierinvoice.SupplierInvoiceID;
            invoiceheader.SupplierID = supplierinvoice.SupplierID;
            invoiceheader.CompanyID = supplierinvoice.CompanyID;
            invoiceheader.BranchID = supplierinvoice.BranchID;
            invoiceheader.InvoiceNo = supplierinvoice.InvoiceNo;
            invoiceheader.TotalAmount = supplierinvoice.TotalAmount;
            invoiceheader.InvoiceDate = supplierinvoice.InvoiceDate;
            invoiceheader.Description = supplierinvoice.Description;
            invoiceheader.UserID = supplierinvoice.UserID;
            invoiceheader.SubTotalAmount = supplierinvoice.SubTotalAmount;
            invoiceheader.EstimatedTax = supplierinvoice.EstimatedTax;
            invoiceheader.ShippingFee = supplierinvoice.ShippingFee;
            purchaseinvoice.InvoiceHeader = invoiceheader;

            //Set Purchase Invoice Branch
            var branch = new BranchMV();

            branch.BranchName = supplierinvoice.tblBranch.BranchName;
            branch.BranchContact = supplierinvoice.tblBranch.BranchContact;
            branch.BranchAddress = supplierinvoice.tblBranch.BranchAddress;
            purchaseinvoice.branch = branch;

            //Set Purchase Invoice Supplier
            var supplier = new SupplierMV();
            supplier.SupplierName = supplierinvoice.tblSupplier.SupplierName;
            supplier.SupplierConatctNo = supplierinvoice.tblSupplier.SupplierConatctNo;
            supplier.SupplierAddress = supplierinvoice.tblSupplier.SupplierAddress;
            supplier.SupplierEmail = supplierinvoice.tblSupplier.SupplierEmail;
            purchaseinvoice.supplier = supplier;

            var purchaseitems = new List<SupplierInvoiceDetailMV>();
            foreach (var item in supplierinvoice.tblSupplierInvoiceDetails)
            {
                var product = new SupplierInvoiceDetailMV();
                product.SupplierInvoiceDetailID = item.SupplierInvoiceDetailID;
                product.SupplierInvoiceID = item.SupplierInvoiceID;
                product.ProductID = item.ProductID;
                product.ProductName = item.tblStock.ProductName;
                product.PurchaseQuantity = item.PurchaseQuantity;
                product.purchaseUnitPrice = item.purchaseUnitPrice;
                product.PreviousPurchaseUnitPrice = item.PreviousPurchaseUnitPrice;
                product.ManufactureDate = item.ManufactureDate;
                product.ExpiryDate = item.ExpiryDate;
                product.ItemCost = (item.PurchaseQuantity * item.purchaseUnitPrice);
                purchaseitems.Add(product);
            }

                var supplierpayment = DB.tblSupplierPayments.Where(s=>s.SupplierInvoiceID == supplierinvoice.SupplierInvoiceID).ToList();
                
                if(supplierpayment!=null)
                {
                    if(supplierpayment.Count() > 0)
                    {
                        purchaseinvoice.PaidAmount = supplierpayment.Sum(s=>s.PaymentAmount);
                    }
                }
                
                purchaseinvoice.InvoiceDetails = purchaseitems;
                purchaselist.Add(purchaseinvoice);
            }
            return View(purchaselist);
        }
        public ActionResult AllSales(FilterModel fm)
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


            if (fm.DateFrom == null)
            {
                ViewBag.DateFrom = System.DateTime.Today.ToString("s");
                fm.DateFrom = System.DateTime.Today;
            }
            else
            {
                ViewBag.DateFrom = Convert.ToDateTime(fm.DateFrom).ToString("s");
            }

            if (fm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Now.ToString("s");
                fm.DateTo = System.DateTime.Now;
            }
            else
            {
                ViewBag.DateTo = Convert.ToDateTime(fm.DateTo).ToString("s");
            }

            ViewBag.Category = DB.tblCategories.Where(x=>x.CompanyID == companyid && x.BranchID == branchid).Select(x => new SelectListItem { Value = x.CategoryID.ToString(), Text = x.categoryName }).ToList();

            if (fm.Category == null)
            {
                ViewBag.Product = DB.tblStocks.Where(x => x.CompanyID == companyid && x.BranchID == branchid).Select(x => new SelectListItem { Value = x.ProductID.ToString(), Text = x.ProductName + " (" + x.Description + ")" }).ToList();
            }
            else
            {
                ViewBag.Product = DB.tblStocks.Where(x => x.CategoryID == fm.Category && x.CompanyID == companyid && x.BranchID == branchid).Select(x => new SelectListItem { Value = x.ProductID.ToString(), Text = x.ProductName + " (" + x.Description + ")" }).ToList();
            }

            var od = DB.tblOrderDetails.Where(x => x.CompanyFID == companyid && x.BranchFID == branchid).Select(x => x.OrderFID).ToList();

            if (fm.Category != null)
            {
                var p = DB.tblStocks.Where(x => x.CategoryID == fm.Category && x.CompanyID == companyid && x.BranchID == branchid).Select(x => x.ProductID).ToList();
                if (fm.Product != null)
                {
                    p = DB.tblStocks.Where(x => x.ProductID == fm.Product && x.CompanyID == companyid && x.BranchID == branchid).Select(x => x.ProductID).ToList();
                }
                od = DB.tblOrderDetails.Where(x => p.Contains(x.ProductFID) && x.CompanyFID == companyid && x.BranchFID == branchid).Select(x => x.OrderFID).ToList();
            }

            var salelist = new List<OrderMV>();
            var allsales = DB.tblOrders.Where(x=>x.OrderType =="Sale" && x.OrderDate >= fm.DateFrom && x.OrderDate <= fm.DateTo && od.Contains(x.OrderID)).ToList();
            foreach (var sale in allsales)
            {
                var order = new OrderMV();
                order.OrderID = sale.OrderID;
                order.OrderDate = sale.OrderDate;
                order.OrderStatus = sale.OrderStatus;
                order.OrderType = sale.OrderType;
                order.OrderName = sale.OrderName;
                order.OrderEmail = sale.OrderEmail;
                order.OrderContact = sale.OrderContact;
                order.OrderAddress = sale.OrderAddress;
                salelist.Add(order);
         }
                return View(salelist);
        }
        public ActionResult PrintSaleInvoice(int ? id)
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

           
            var o = DB.tblOrders.Where(O=>O.OrderID==id).ToList();
            return View(o);
        }
        public ActionResult ProfitAndLossReport(FilterModel fm)
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

            if (fm.DateFrom == null)
            {
                ViewBag.DateFrom = System.DateTime.Today.ToString("s");
                fm.DateFrom = System.DateTime.Today;
            }
            else
            {
                ViewBag.DateFrom = Convert.ToDateTime(fm.DateFrom).ToString("s");
            }

            if (fm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Now.ToString("s");
                fm.DateTo = System.DateTime.Now;
            }
            else
            {
                ViewBag.DateTo = Convert.ToDateTime(fm.DateTo).ToString("s");
            }

            ViewBag.Category = DB.tblCategories.Where(x => x.CompanyID == companyid && x.BranchID == branchid).Select(x => new SelectListItem { Value = x.CategoryID.ToString(), Text = x.categoryName }).ToList();

            if (fm.Category == null)
            {
                ViewBag.Product = DB.tblStocks.Where(x => x.CompanyID == companyid && x.BranchID == branchid).Select(x => new SelectListItem { Value = x.ProductID.ToString(), Text = x.ProductName + " (" + x.Description + ")" }).ToList();
            }
            else
            {
                ViewBag.Product = DB.tblStocks.Where(x => x.CategoryID == fm.Category && x.CompanyID == companyid && x.BranchID == branchid).Select(x => new SelectListItem { Value = x.ProductID.ToString(), Text = x.ProductName + " (" + x.Description + ")" }).ToList();
            }

            var od = DB.tblOrderDetails.Where(x => x.CompanyFID == companyid && x.BranchFID == branchid).Select(x => x.OrderFID).ToList();

            if (fm.Category != null)
            {
                var p = DB.tblStocks.Where(x => x.CategoryID == fm.Category && x.CompanyID == companyid && x.BranchID == branchid).Select(x => x.ProductID).ToList();
                if (fm.Product != null)
                {
                    p = DB.tblStocks.Where(x => x.ProductID == fm.Product && x.CompanyID == companyid && x.BranchID == branchid).Select(x => x.ProductID).ToList();
                }
                od = DB.tblOrderDetails.Where(x => p.Contains(x.ProductFID) && x.CompanyFID == companyid && x.BranchFID == branchid).Select(x => x.OrderFID).ToList();
            }

            //var salelist = new List<OrderMV>();
            var allsales = DB.tblOrders.Where(x => x.OrderType == "Sale" && x.OrderDate >= fm.DateFrom && x.OrderDate <= fm.DateTo && od.Contains(x.OrderID)).ToList();
            //foreach (var sale in allsales)
            //{
            //    var order = new OrderMV();
            //    order.OrderID = sale.OrderID;
            //    order.OrderDate = sale.OrderDate;
            //    order.OrderStatus = sale.OrderStatus;
            //    order.OrderType = sale.OrderType;
            //    order.OrderName = sale.OrderName;
            //    order.OrderEmail = sale.OrderEmail;
            //    order.OrderContact = sale.OrderContact;
            //    order.OrderAddress = sale.OrderAddress;
            //    salelist.Add(order);
            //}
            return View(allsales);
            
        }
        public ActionResult StockReport(FilterModel fm)
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



            //kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk

            if (fm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Now.ToString("s");
                fm.DateTo = System.DateTime.Now;
            }
            else
            { ViewBag.DateTo = Convert.ToDateTime(fm.DateTo).ToString("s"); }

            ViewBag.Category = DB.tblCategories.Where(x=>x.CompanyID == companyid && x.BranchID == branchid).Select(x => new SelectListItem { Value = x.CategoryID.ToString(), Text = x.categoryName }).ToList();

            if (fm.Category == null)
            {
                ViewBag.Product = DB.tblStocks.Where(x => x.CompanyID == companyid && x.BranchID == branchid).Select(x => new SelectListItem { Value = x.ProductID.ToString(), Text = x.ProductName + " (" + x.Description + ")" }).ToList();
            }
            else
            {
                ViewBag.Product = DB.tblStocks.Where(x => x.CompanyID == companyid && x.BranchID == branchid).Where(x => x.CategoryID == fm.Category).Select(x => new SelectListItem { Value = x.ProductID.ToString(), Text = x.ProductName + " (" + x.Description + ")" }).ToList();
            }

            var p = DB.tblStocks.Where(x => x.CompanyID == companyid && x.BranchID == branchid).ToList();

            if (fm.Category != null)
            {
                p = DB.tblStocks.Where(x => x.CategoryID == fm.Category && x.CompanyID == companyid && x.BranchID == branchid).ToList();
                if (fm.Product != null)
                {
                    p = DB.tblStocks.Where(x => x.ProductID == fm.Product && x.CompanyID == companyid && x.BranchID == branchid).ToList();
                }

            }
            //kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk
                return View(p);
        }
        public JsonResult GetProduct(int? categoryID)
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

            var list = new List<tblStock>();
            var stocks = DB.tblStocks.Where(a => a.CompanyID == companyid && a.BranchID == branchid && a.CategoryID == categoryID).ToList();
            foreach (var stock in stocks)
            {
                var c = new tblStock();

                c.ProductID = stock.ProductID;
                c.ProductName = stock.ProductName;
                list.Add(c);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}