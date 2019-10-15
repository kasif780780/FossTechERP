using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FossERp.Models
{
    public class PurchaseOrderProduct
    {
        public int Id { get; set; }
   
        public int ProductId { get; set; }
        public int PurchaseOrderId { get; set; }
        public int Qty { get; set; }

        public string Status { get; set; }

        public virtual Product Product { get; set; }
   
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}