using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class EmployeeMV
    {
        public int EmployeeID { get; set; }
        [Required(ErrorMessage ="Required *")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required *")]
        public string ContactNo { get; set; }
       
        public string Photo { get; set; }
        [Required(ErrorMessage = "Required *")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required *")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Required *")]
        public string CNIC { get; set; }
        [Required(ErrorMessage = "Required *")]
        public string Designation { get; set; }
        [Required(ErrorMessage = "Required *")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Required *")]
        public double MonthlySalary { get; set; }
        public int BranchID { get; set; }
        public int CompanyID { get; set; }
        public Nullable<int> UserID { get; set; }
    }
}