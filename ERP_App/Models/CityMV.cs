using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class CityMV
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }
        public string CityZone { get; set; }
        public string CityCode { get; set; }
        public string CityPinCode { get; set; }
    }
}