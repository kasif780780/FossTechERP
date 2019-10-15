using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace FossERp.Models
{
    [Table("Payroll")]
    public class Payroll
    {
        public int PayrollID { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }
        [Required]
        [Display(Name ="Payment Type")]
        public string PaymentType { get; set; }

        [Display(Name="Bill")]
        public string ImagePath { get; set; }
        [Required]
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public Payroll()
        {
            PaymentDate = DateTime.Now;
        }
    }
}