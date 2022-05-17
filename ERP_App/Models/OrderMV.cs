using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class OrderMV
    {
        public int OrderID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string OrderType { get; set; }
        public string OrderName { get; set; }
        public string OrderEmail { get; set; }
        public string OrderContact { get; set; }
        public string OrderAddress { get; set; }
    }
}