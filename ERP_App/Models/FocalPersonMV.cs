using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class FocalPersonMV
    {
        public EmployeeMV employeeMV { get; set; } //saray alag alag se create na kerein properties, ayse create ker lein.
        public UserMV userMV { get; set; }
        public int BranchID { get; set; } //kis branch mein hum add ker rehay hein employee ko.
        public int CompanyID { get; set; }//kis company mein hum add ker rehay hein employee ko.
    }
}