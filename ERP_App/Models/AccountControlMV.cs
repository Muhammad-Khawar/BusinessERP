using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class AccountControlMV
    {
        public int AccountControlID { get; set; }
        public int CompanyID { get; set; }
      
        [Display(Name ="Company")]
        public string CompanyName { get; set; }
        public int BranchID { get; set; }
              [Display(Name = "Branch")]
        public string BranchName { get; set; }
        [Required(ErrorMessage = "Required*")]
        public int AccountHeadID { get; set; }
      
        [Display(Name = "Account Head")]
        public string AccountHead { get; set; }
        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Account Control")]
        public string AccountControlName { get; set; }
        public int UserID { get; set; }
       
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
    }
}