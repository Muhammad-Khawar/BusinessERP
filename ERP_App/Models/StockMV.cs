using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class StockMV
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double SaleUnitPrice { get; set; }
        public double CurrentPurchaseUnitPrice { get; set; }
        public System.DateTime ExpiryDate { get; set; }
        public System.DateTime Manufacture { get; set; }
        public int StockTreshHoldQuantity { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        public bool IsActive { get; set; }

    }
}