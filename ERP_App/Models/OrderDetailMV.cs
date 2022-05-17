using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class OrderDetailMV
    {
        public int OrderDetailID { get; set; }
        public Nullable<int> OrderFID { get; set; }
        public Nullable<int> ProductFID { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public Nullable<decimal> PurchasePrice { get; set; }
        public Nullable<int> Quantity { get; set; }

    }
}