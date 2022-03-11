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
        [NotMapped]
        public HttpPostedFileBase PhotoFile { get; set; }
    }
}