using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class BranchTypeMV
    {
        public int BranchTypeID { get; set; }
        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Branch Type")]
        public string BranchType { get; set; }
    }
}