using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class PrintPurchaseInvoiceMV
    {
        public BranchMV branch { get; set; }
        public SupplierMV supplier { get; set; }
        public SupplierInvoiceMV InvoiceHeader { get; set; }
        public List<SupplierInvoiceDetailMV> InvoiceDetails { get; set; }
        public double PaidAmount { get; set; }
    
    }
}