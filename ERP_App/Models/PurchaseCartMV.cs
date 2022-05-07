using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class PurchaseCartMV
    {
        public StockMV SelectPurchaseItem { get; set; }
        public List<PurchaseItemsMV> PurchaseItemList { get; set; }
    }
}