using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class AccountActivityMV
    {
        public int AccountActivityID { get; set; }
        [Required(ErrorMessage ="Required*")]
        [Display(Name ="Account Activity")]
        public string Name { get; set; }
    }
}