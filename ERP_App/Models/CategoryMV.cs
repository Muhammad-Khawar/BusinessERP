using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class CategoryMV
    {
        public int CategoryID { get; set; }
        [Display(Name = "Category")]
        public string categoryName { get; set; }
        public int BranchID { get; set; }
        public int CompanyID { get; set; }
        public int UserID { get; set; }
        [Display(Name ="Created By")]
        public string CreatedBy { get; set; }
        public int ProductCount { get; set; }
    }
}