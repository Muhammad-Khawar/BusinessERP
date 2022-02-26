using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class CustomerInvoiceDetailMV
    {
        public int CustomerInvoiceDetailID { get; set; }
        public int CustomerInvoiceID { get; set; }
        public int ProductID { get; set; }
        public int SaleQuantity { get; set; }
        public double SaleUnitPrice { get; set; }
    }
}