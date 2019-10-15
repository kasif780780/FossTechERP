using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FossERp.Models
{
    public class Vendor
    {
        public int VendorID { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public decimal? Gst { get; set; }
        public decimal? OpeningBalance { get; set; }
        public string Address { get; set; }

        public IList<PurchaseOrder> PurchaseOrders { get; set; }
    }
}