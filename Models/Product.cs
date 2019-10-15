using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FossERp.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
       
       [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
       
        public DateTime SalesStartDate { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public decimal Tax { get; set; }
        [Required]
        public string Unit { get; set; }
        [Required]
        public string Description { get; set; }
        public string HSNORSACCode { get; set; }

        public bool IsActive { get; set; }
        public Product()
        {
            SalesStartDate = DateTime.Now;

        }
    }
}