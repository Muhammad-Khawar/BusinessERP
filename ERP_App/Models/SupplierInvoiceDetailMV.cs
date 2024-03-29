﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class SupplierInvoiceDetailMV
    {
        public int SupplierInvoiceDetailID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int PurchaseQuantity { get; set; }
        public double purchaseUnitPrice { get; set; }
        public double PreviousPurchaseUnitPrice { get; set; }
        public System.DateTime ManufactureDate { get; set; }
        public System.DateTime ExpiryDate { get; set; }
        public double ItemCost { get; set; }

    }
}