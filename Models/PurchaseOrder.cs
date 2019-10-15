using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FossERp.Models
{
    [Table("PurchaseOrder")]
    public class PurchaseOrder
    {
        [Key]
        public int PurchaseOrderId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
   
     
        public int VendorID { get; set; }
        public string Status { get; set; }

       
        public virtual Vendor Vendor { get; set; }
        public virtual List<PurchaseOrderProduct> Products { get; set; }


        public PurchaseOrder()
        {
            Date = DateTime.Now;
        }
    }
}