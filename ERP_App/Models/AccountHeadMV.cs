using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class AccountHeadMV
    {
        public int AccountHeadID { get; set; }

        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Account Head")]
        public string AccountHeadName { get; set; }
        public int Code { get; set; }
        public int UserID { get; set; }

        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
    }
}