using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class CompanyRequestMV
    {
        //Company
        public int CompanyID { get; set; }
        public string Company { get; set; }
        //Branch
        public int BranchID { get; set; }
        public int BranchTypeID { get; set; }
        public string BranchType { get; set; }
        public string BranchName { get; set; }
        public string BranchContact { get; set; }
        public string BranchAddress { get; set; }
        public Nullable<int> BrchID { get; set; }
        //Employee
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string ContactNo { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CNIC { get; set; }
        public string Designation { get; set; }
        public string Description { get; set; }
        public double MonthlySalary { get; set; }
        //User
        public int UserID { get; set; }
        public int UserTypeID { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
    }
}