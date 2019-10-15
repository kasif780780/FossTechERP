using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FossERp.Models
{
    public class Lead
    {
        [Key]
        public int LeadID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name ="Lead Creation Date")]
        public DateTime LeadCreationDate { get; set; }
        [Required]
        [Display(Name ="Contact Name")]
        public string ContactName { get; set; }
        [Display(Name = "Contact Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Contact Mobile")]
        public string Mobile { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Company Email")]
        public string CompanyEmail { get; set; }
        [Display(Name = "Company Mobile")]
        public string CompanyMobile { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "LandMark")]
        public string LandMark { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        [Required]
        [Display(Name = "Pincode")]
        public string Pincode { get; set; }
        [Display(Name = "Lead Creator Name")]
        public string LeadCreatorName { get; set; }
        [Display(Name = "Lead Source")]
        public string Source { get; set; }
        [Required]
        [Display(Name = "Business Category")]
        public string BusinessCategory { get; set; }
        [Display(Name = "Our Product")]
        public string OurProduct { get; set; }

        public virtual IList<SalesOrder> SalesOrders { get; set; }

        public Lead()
        {
            LeadCreationDate = DateTime.Now;
        }


    }
}