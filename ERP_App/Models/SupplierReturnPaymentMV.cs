using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class SupplierReturnPaymentMV
    {
        public int SupplierReturnPaymentID { get; set; }
        public int SupplierReturnInvoiceID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public int SupplierID { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public string InvoiceNo { get; set; }
        public double TotalAmount { get; set; }
        public double PaymentAmount { get; set; }
        public double RemainingBalance { get; set; }
        public int UserID { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
    }
}