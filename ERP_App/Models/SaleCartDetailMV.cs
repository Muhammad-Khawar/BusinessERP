using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class SaleCartDetailMV
    {
        public int SaleCartDetailID { get; set; }
        public int ProductID { get; set; }
        public int SaleQuantity { get; set; }
        public double SaleUnitPrice { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public int UserID { get; set; }
    }
}