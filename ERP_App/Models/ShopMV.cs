using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class ShopMV
    {
        public List<CategoryMV> Cat { get; set; }
        public List<StockMV> Pro { get; set; }
        public double MaxPrice { get; set; }
    }
}