using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class AccountSubControlMV
    {
        public int AccountSubControlID { get; set; }
        public int AccountHeadID { get; set; }

        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Account Head")]
        public string AccountHead { get; set; }
        public int AccountControlID { get; set; }

        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Account Control")]
        public string AccountControl { get; set; }
        public int CompanyID { get; set; }

        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Company")]
        public string Company { get; set; }
        public int BranchID { get; set; }

        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Branch")]
        public string Branch { get; set; }

        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Account Sub Control")]
        public string AccountSubControlName { get; set; }
        public int UserID { get; set; }

        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
    }
}