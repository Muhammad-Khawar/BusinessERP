using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class PurchaseItemsMV
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "*Required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Sale Price")]
        public double SaleUnitPrice { get; set; }

        [Display(Name = "Previous Purchase Unit Price")]
        public double PreviousPurchaseUnitPrice { get; set; }

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Cuurent Purchase Unit Price")]
        public double CurrentPurchaseUnitPrice { get; set; }
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Expire Date")]
        [DataType(DataType.Date)]
        public System.DateTime ExpiryDate { get; set; }
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Manufacture Date")]
        [DataType(DataType.Date)]
        public System.DateTime Manufacture { get; set; }
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Stock Treshhold Quantity")]
        public int StockTreshHoldQuantity { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }
       

    }
}