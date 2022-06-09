using ERP.DatabaseLayer;
using ERP_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class HomeController : Controller
    {
        private BusinessERP_DBEntities DB = new BusinessERP_DBEntities();
        // GET: Home
        public ActionResult IndexCustomer(int? id)
        {
            //var qty = DB.tblStocks.Where(x => x.IsActive == true && x.Quantity > 0).Max(x => x.Quantity);
            var stock = DB.tblStocks.Where(x => x.IsActive == true && x.Quantity > 0).OrderByDescending(x=>x.Quantity).ToList();
            //var stock = DB.tblStocks.Where(x=>x.IsActive==true && x.Quantity > 0).OrderByDescending(x=>x.ProductID).ToList();
            if (id!=null)
            {
                stock = DB.tblStocks.Where(x=>x.ProductID ==id && x.IsActive == true && x.Quantity > 0).OrderByDescending(x => x.Quantity).ToList();
            }
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
                item.ProductPicture = product.ProductPicture;
                list.Add(item);
            }
            FilterProductMV p = new FilterProductMV();
            p.Products = list;
            return View(p);
        }
        public ActionResult Cart(int ? id)
        {
            var stock = DB.tblStocks.Where(x => x.IsActive == true && x.Quantity > 0).OrderByDescending(x => x.Quantity).ToList();
            if (id != null)
            {
                stock = DB.tblStocks.Where(x => x.ProductID == id && x.IsActive == true && x.Quantity > 0).OrderByDescending(x => x.Quantity).ToList();
            }
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
                item.ProductPicture = product.ProductPicture;
                list.Add(item);
            }
            FilterProductMV p = new FilterProductMV();
            p.Products = list;
            return View(p);
        }
        public ActionResult AddToCart(int id)
        {
            List<StockMV> list;
            if (Session["myCart"] == null)
            {
                list = new List<StockMV>();
            }
            else
            {
                list = (List<StockMV>)Session["myCart"];
            }
            //kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk
            Boolean isProductExist = false;
            foreach (var p in list)
            {
                if (id == p.ProductID)
                {
                    isProductExist = true;
                    p.Quantity++;
                }
            }

            if (isProductExist == false)
            {
                var stock = DB.tblStocks.Where(p => p.ProductID == id).FirstOrDefault();

                var item = new StockMV();
                item.BranchID = stock.BranchID;
                item.CategoryID = stock.CategoryID;
                item.CompanyID = stock.CompanyID;
                item.CreatedBy = stock.tblUser.UserName;
                item.CurrentPurchaseUnitPrice = stock.CurrentPurchaseUnitPrice;
                item.Description = stock.Description;
                item.Manufacture = stock.Manufacture;
                item.ExpiryDate = stock.ExpiryDate;
                item.IsActive = stock.IsActive;
                item.ProductID = stock.ProductID;
                item.ProductName = stock.ProductName;
                item.Quantity = stock.Quantity;
                item.SaleUnitPrice = stock.SaleUnitPrice;
                item.StockTreshHoldQuantity = stock.StockTreshHoldQuantity;
                item.UserID = stock.UserID;
                item.CategoryName = stock.tblCategory.categoryName;
                item.ProductPicture = stock.ProductPicture;

                list.Add(item);
                list[list.Count - 1].Quantity = 1;
            }
            Session["myCart"] = list;
            //kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk

            
            Session["myCart"] = list;
            return RedirectToAction("Cart");
        }
        public ActionResult PlusToCart(int RowNo)
        {
            List<StockMV> list = (List<StockMV>)Session["myCart"];
            int P_ID = list[RowNo].ProductID;
            //int? available = DB.tblOrderDetails.Where(x => x.ProductFID == P_ID).Sum(x => x.Quantity);
            int? available = DB.tblStocks.Where(x => x.ProductID == P_ID).Select(x=>x.Quantity).FirstOrDefault();
            if (available > list[RowNo].Quantity)
            {
                list[RowNo].Quantity++;
            }
            Session["myCart"] = list;
            return RedirectToAction("Cart");
        }
        public ActionResult MinusToCart(int RowNo)
        {
            List<StockMV> list = (List<StockMV>)Session["myCart"];
            list[RowNo].Quantity--;
            if (list[RowNo].Quantity <= 0)
            {
                //list.RemoveAt(RowNo);
                list[RowNo].Quantity = 0;
            }
            Session["myCart"] = list;
            return RedirectToAction("Cart");
        }
        public ActionResult RemoveFromCart(int RowNo)
        {
            List<StockMV> list = (List<StockMV>)Session["myCart"];
            list.RemoveAt(RowNo);
            Session["myCart"] = list;
            return RedirectToAction("Cart");
        }
        public ActionResult Shop(int ? id)
        {
           
            ShopMV s = new ShopMV();
            
            var Categorieslist = new List<CategoryMV>();
            var categories = DB.tblCategories.ToList();
            foreach (var category in categories)
            {
                var username = category.tblUser.UserName;
                
                Categorieslist.Add(new CategoryMV
                {
                    BranchID = category.BranchID,
                    CategoryID = category.CategoryID,
                    categoryName = category.categoryName,
                    CompanyID = category.CompanyID,
                    UserID = category.UserID,
                    ProductCount = category.tblStocks.Count(),
                    CreatedBy = username
                });
            }
            s.Cat = Categorieslist;
     
            if (id == null)
            {
                var stock = DB.tblStocks.Where(x=>x.Quantity > 0).OrderByDescending(x => x.Quantity).ToList();
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
                    item.ProductPicture = product.ProductPicture;
                    list.Add(item);
                }

                s.Pro = list;
            }
            else
            {
                var stock = DB.tblStocks.Where(p => p.CategoryID == id && p.Quantity > 0).OrderByDescending(x => x.Quantity).ToList();
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
                    item.ProductPicture = product.ProductPicture;
                    list.Add(item);
                }

                s.Pro = list;
            }
            var max = DB.tblStocks.Max(x=>x.SaleUnitPrice);
            s.MaxPrice = max;
            return View(s);
        }
        public ActionResult FilterProducts(string searchTerm,int? CategoryId,int ? minimumPrice,int? maximunPrice)
        {
            FilterProductMV model = new FilterProductMV();
            //kkkkkkkkkkkkkkkkkkkkkkkkk
            var stock = DB.tblStocks.Where(x=>x.Quantity > 0).OrderByDescending(x => x.Quantity).ToList();
            if(!string.IsNullOrEmpty(searchTerm))
            {
                stock = stock.Where(x => x.ProductName.ToLower().Contains(searchTerm.ToLower()) && x.Quantity > 0).OrderByDescending(x => x.Quantity).ToList();
            }
            if(CategoryId.HasValue)
            {
                stock = stock.Where(x => x.CategoryID == CategoryId).OrderByDescending(x => x.Quantity).ToList();
            }
            if(minimumPrice.HasValue)
            {
                stock = stock.Where(x => x.SaleUnitPrice >= minimumPrice.Value && x.Quantity > 0).OrderByDescending(x => x.Quantity).ToList();
            }
            if(maximunPrice.HasValue)
            {
                stock = stock.Where(x => x.SaleUnitPrice <= maximunPrice.Value && x.Quantity > 0).OrderByDescending(x => x.Quantity).ToList();
            }
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
                item.ProductPicture = product.ProductPicture;
                list.Add(item);
            }
            //kkkkkkkkkkkkkkkkkkkkkkkk
            model.Products = list;
            return PartialView(model);
        }
        public ActionResult PayNow(OrderMV o)
        {
            o.OrderDate = DateTime.Now;
            //o.OrderStatus = "Paid";
            o.OrderType = "Sale";
            o.Status = "Active";
            if (Session["Customer"] != null)
            {
                tblCustomer c = (tblCustomer)Session["Customer"];
                o.CustomerFID = c.CustomerID;
            }
            Session["Order"] = o;
            if(o.OrderStatus == "Cash on Delivery")
            {
                return RedirectToAction("OrderBooked");
            }
            else
            {
                return Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=khawarbt17108@gmail.com&item_name=SoleBoxShopProducts&return=http://localhost:59656/Home/OrderBooked&amount=" + double.Parse(Session["totalamount"].ToString()) / 190);
            }    
        }
        public ActionResult OrderBooked()
        {
            OrderMV o = (OrderMV)Session["Order"];
            //1.send email
            //MailMessage mail = new MailMessage();
            //mail.From = new MailAddress("khawargsabir@gmail.com");
            //mail.To.Add(o.OrderEmail);
            //mail.Subject = "Order Confirmation";
            //mail.Body = "<b>Thanks For Your Order!</b> Your Order will be delivered as per schedule - TheSoleBoxShop";
            //mail.IsBodyHtml = true;

            //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            //SmtpServer.Port = 587;
            //SmtpServer.Credentials = new System.Net.NetworkCredential("khawargsabir@gmail.com", "09GNIK87");
            //SmtpServer.EnableSsl = true;
            //SmtpServer.Send(mail);

            //2. send SMS
            //String api = "https://lifetimesms.com/json?api_token=1e68a08b0558ffb61b47192772f84ca6661d2d7151&api_secret=TheSoleBoxShop&to=" + o.OrderContact + "&from=TheSoleBoxShop&message=Order Confirmation";
            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(api);
            //var httpResponse = (HttpWebResponse)req.GetResponse();

            //3. save in DB
            var order = new tblOrder();

            order.OrderDate = o.OrderDate;
            order.OrderStatus = o.OrderStatus;
            order.OrderType = o.OrderType;
            order.OrderName = o.OrderName;
            order.OrderEmail = o.OrderEmail;
            order.OrderContact = o.OrderContact;
            order.OrderAddress = o.OrderAddress;
            order.Status = o.Status;
            order.CustomerFID = o.CustomerFID;
            DB.tblOrders.Add(order);
            DB.SaveChanges();

            List<StockMV> p = (List<StockMV>)Session["myCart"];
            for (int i = 0; i < p.Count; i++)
            {
                //kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk
                var stockproduct = DB.tblStocks.Find(p[i].ProductID);
                stockproduct.Quantity = stockproduct.Quantity - p[i].Quantity;
                DB.Entry(stockproduct).State = System.Data.Entity.EntityState.Modified;
                DB.SaveChanges();
                //kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk
                //kkkkkkkkkkkkkkkkkkkkkkkkkkkkk
                
                //kkkkkkkkkkkkkkkkkkkkkkkkkkkkkk

                var od = new tblOrderDetail();
                int orderID = DB.tblOrders.Max(x => x.OrderID);
                od.OrderFID = orderID;
                od.ProductFID = p[i].ProductID;
                od.Quantity = p[i].Quantity*-1;
                od.PurchasePrice = (decimal)p[i].CurrentPurchaseUnitPrice;
                od.SalePrice = (decimal)p[i].SaleUnitPrice;
                od.CompanyFID = p[i].CompanyID;
                od.BranchFID = p[i].BranchID;
                DB.tblOrderDetails.Add(od);
                DB.SaveChanges();

            }

            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult Feedback()
        {
            return View();
        }
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
        public ActionResult CloseOrder()
        {
            Session["Order"] = null;
            Session["myCart"] = null;
            return RedirectToAction("IndexCustomer");
        }
    }
}