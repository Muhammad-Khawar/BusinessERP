using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class SupplierReturnInvoiceMV
    {
        public int SupplierReturnInvoiceID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public int SupplierID { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public string InvoiceNo { get; set; }
        public double TotalAmount { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
    }
}