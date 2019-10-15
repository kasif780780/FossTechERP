using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FossERp.Models
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string Status { get; set; }
        
        public virtual Customer Customer { get; set; }

        public virtual List<OrderProduct> Products { get; set; }

        public Order()
        {
            Date = DateTime.Now;
        }
    }
}