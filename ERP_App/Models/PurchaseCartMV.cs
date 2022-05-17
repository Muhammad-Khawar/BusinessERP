using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class PurchaseCartMV
    {
        public int PurchaseCartDetailID { get; set; }
        [Display(Name = "Product")]
        [Required(ErrorMessage = "Required*")]
        public int ProductID { get; set; }
        [Display(Name = "Purchase Quantity")]
        [Required(ErrorMessage = "Required*")]
        public int PurchaseQuantity { get; set; }
        [Display(Name = "Current Purchase Unit Price")]
        [Required(ErrorMessage = "Required*")]
        public double CurrentPurchaseUnitPrice { get; set; }
        [Display(Name = "Sale Unit Price")]
        [Required(ErrorMessage = "Required*")]
        public double SaleUnitPrice { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public int UserID { get; set; }
        public double PrevoiusPurchaseUnitPrice { get; set; }
        public string Description { get; set; }
        [Display(Name = "Manufacture Date")]
        [Required(ErrorMessage = "Required*")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ManufactureDate { get; set; }
        [Display(Name = "Expiry Date")]
        [Required(ErrorMessage = "Required*")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ExpireDate { get; set; }
        [Required(ErrorMessage = "Required*")]
        public int SupplierID { get; set; }
       
        public PurchaseCartSummaryMV OrderSummary { get; set; }
        public List<PurchaseItemsMV> PurchaseItemList { get; set; }
    }
}