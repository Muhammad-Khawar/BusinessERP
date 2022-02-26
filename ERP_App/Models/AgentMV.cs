using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class AgentMV
    {
        public int AgentID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string AgentName { get; set; }
        public string ContactNo { get; set; }
        public string PhoneNo { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public double Agent_Commission { get; set; }
        public int CountryID { get; set; }
        public int StateID { get; set; }
        public int CityID { get; set; }
        public int BranchID { get; set; }
    }
}