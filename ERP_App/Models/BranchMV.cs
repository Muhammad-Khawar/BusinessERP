using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class BranchMV
    {
        public int BranchID { get; set; }

        [Required(ErrorMessage = "Required*")]
        public int BranchTypeID { get; set; }

        [Display(Name = "Branch Type")]
        public string BranchType { get; set; }

        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Branch")]
        public string BranchName { get; set; }

        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Contact No")]
        public string BranchContact { get; set; }

        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Address")]
        public string BranchAddress { get; set; }
        [Required(ErrorMessage = "Required*")]
        public int CompanyID { get; set; }

      
        [Display(Name = "Company")]
        public string Company { get; set; }
        public Nullable<int> BrchID { get; set; }
    }
}