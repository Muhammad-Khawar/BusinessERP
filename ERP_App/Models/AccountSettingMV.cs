using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class AccountSettingMV
    {
        public int AccountSettingID { get; set; }
        [Required(ErrorMessage = "Required*")]
        public int AccountHeadID { get; set; }
        
        [Display(Name = "Account Head")]
        public string AccountHead { get; set; }
        [Required(ErrorMessage = "Required*")]
        public int AccountControlID { get; set; }

        [Display(Name = "Account Control")]
        public string AccountControl { get; set; }
        [Required(ErrorMessage = "Required*")]
        public int AccountSubControlID { get; set; }

        [Display(Name = "Account Sub Control")]
        public string AccountSubControl { get; set; }
        [Required(ErrorMessage = "Required*")]
        public int AccountActivityID { get; set; }

        [Display(Name = "Account Activity")]
        public string AccountActivity { get; set; }
        public int CompanyID { get; set; }

        [Display(Name = "Company")]
        public string Company { get; set; }
        public int BranchID { get; set; }

        [Display(Name = "Branch")]
        public string Branch { get; set; }
    }
}