using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FossERp.Models
{
    public class Expense
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Currency")]
        public string Currency { get; set; }
        [Required]
        [Display(Name ="Amount")]
        public decimal? Amount { get; set; }
        [Required]
        [Display(Name ="Purpose")]
        public string Purpose { get; set; }
        [Required]
        [Display(Name ="Date of Spend")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfSpend { get; set; }

        [Display(Name = "Marchant")]
        public string Marchant { get; set; }
        [Required]
        [Display(Name ="Categories")]
        public string Category { get; set; }
        [Display(Name ="Description")]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        public Expense()
        {
            DateOfSpend = DateTime.Now;
        }
    }
}