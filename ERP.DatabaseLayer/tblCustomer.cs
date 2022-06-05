//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP.DatabaseLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCustomer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCustomer()
        {
            this.tblCustomerInvoices = new HashSet<tblCustomerInvoice>();
            this.tblCustomerReturnInvoices = new HashSet<tblCustomerReturnInvoice>();
            this.tblCustomerReturnPayments = new HashSet<tblCustomerReturnPayment>();
            this.tblOrders = new HashSet<tblOrder>();
        }
    
        public int CustomerID { get; set; }
        public string Customername { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPassword { get; set; }
        public string CustomerArea { get; set; }
        public string CustomerAddress { get; set; }
        public string Description { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> UserID { get; set; }
    
        public virtual tblBranch tblBranch { get; set; }
        public virtual tblCompany tblCompany { get; set; }
        public virtual tblUser tblUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCustomerInvoice> tblCustomerInvoices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCustomerReturnInvoice> tblCustomerReturnInvoices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCustomerReturnPayment> tblCustomerReturnPayments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOrder> tblOrders { get; set; }
    }
}
