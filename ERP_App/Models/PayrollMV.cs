using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class PayrollMV
    {
        public int PayrollID { get; set; }
        public int EmployeeID { get; set; }
        public int BranchID { get; set; }
        public int CompanyID { get; set; }
        public double TransferAmount { get; set; }
        public string PayrollInvoiceNo { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public string SalaryMonth { get; set; }
        public string SalaryYear { get; set; }
        public int UserID { get; set; }

    }
}