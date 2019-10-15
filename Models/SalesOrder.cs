using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FossERp.Models
{
    [Table("SalesOrder")]
    public class SalesOrder
    {
        [Key]
        public int SalesOrderID { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "S.O. Confirmation Date")]
        public string SaleConfirmationDate { get; set; }

       

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

     


        [Display(Name = "Sales Person")]
        public string SalesPerson { get; set; }

        [Display(Name ="Total Price (inc taxes)")]
        public decimal TotalPrice { get; set; }

        [Display(Name ="MIB Status")]
        public string MIBStatus { get; set; }

        [Display(Name = "MIB Hold Reason")]
        public string MIBHold { get; set; }

        [Display(Name ="Activation & Invoice Status")]
        public string activation { get; set; }

      

        public int LeadID { get; set; }

        public virtual Lead Lead { get; set; }

    }
}