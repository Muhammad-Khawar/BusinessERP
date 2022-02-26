using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class TransactionMV
    {
        public int TransactionID { get; set; }
        public int FinancialYearID { get; set; }
        public int AccountHeadID { get; set; }
        public int AccountControlID { get; set; }
        public int AccountSubControlID { get; set; }
        public string InvoiceNo { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public System.DateTime TransectionDate { get; set; }
        public string TransectionTitle { get; set; }
        public int UserID { get; set; }

    }
}