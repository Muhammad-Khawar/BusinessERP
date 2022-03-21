using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class FinancialYearMV
    {
        public int FinancialYearID { get; set; }
        [Display(Name ="Financial Year")]
        public string FinancialYear { get; set; }
        [Display(Name ="Start Date")]
        [DataType(DataType.Date)]
        public System.DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public System.DateTime EndDate { get; set; }
        [Display(Name = "Status")]
        public bool IsActive { get; set; }
        public int UserID { get; set; }
        [Display(Name = "CreatedBy")]
        public string CreatedBy { get; set; }//Kis ne create kiya hai.
    }
}