using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class PurchaseCartDetailMV
    {
        public int PurchaseCartDetailID { get; set; }
        public int ProductID { get; set; }
        public int PurchaseQuantity { get; set; }
        public double purchaseUnitPrice { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public int UserID { get; set; }

    }
}