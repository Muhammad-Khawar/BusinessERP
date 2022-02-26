using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class EmployeeMV
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CNIC { get; set; }
        public string Designation { get; set; }
        public string Description { get; set; }
        public double MonthlySalary { get; set; }
        public int BranchID { get; set; }
        public int CompanyID { get; set; }
        public Nullable<int> UserID { get; set; }
    }
}