using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class CompanyMV
    {
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }

        [NotMapped]
        public HttpPostedFileBase Pro_Pic { get; set; }
    }
}