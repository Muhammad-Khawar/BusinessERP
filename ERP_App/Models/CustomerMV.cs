using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class CustomerMV
    {
        public int CustomerID { get; set; }
        public string Customername { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerArea { get; set; }
        public string CustomerAddress { get; set; }
        public string Description { get; set; }
        public int BranchID { get; set; }
        public int CompanyID { get; set; }
        public int UserID { get; set; }
    }
}