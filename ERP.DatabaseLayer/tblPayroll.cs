//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP.DatabaseLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPayroll
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
    
        public virtual tblBranch tblBranch { get; set; }
        public virtual tblCompany tblCompany { get; set; }
        public virtual tblEmployee tblEmployee { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}
