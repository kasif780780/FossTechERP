using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FossERp.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        [Required]
        [Display(Name ="Employee Name")]
        public string FullName { get; set; }
      
        [Display(Name ="Joining Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime JoiningDate { get; set; }
        [Display(Name ="Job Title")]
        public string JobTitle { get; set; }
        [Required]
        [Display(Name ="Mobile No.")]
        public string Mobile { get; set; }
        [Required]
        [Display(Name ="Email")]
        public string Email { get; set; }
        [Display(Name ="Department")]
        public string Department { get; set; }
        [Display(Name ="Position")]
        public string Position { get; set; }

        [Display(Name ="Address")]
        public string Address { get; set; }
        [Display(Name ="Loction")]
        public string Location { get; set; }
        [Display(Name ="Bank Account")]
        public string BankAccount { get; set; }
        [Display(Name = "IFSC Code")]
        public string BankIFSC { get; set; }
        [Display(Name="Gender")]
        public string Gender { get; set; }
        [Display(Name ="Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateofBirth { get; set; }

        [Display(Name ="Employee Identification Number")]
        public string IdentificationNo { get; set; }

        [Display(Name ="Certificate Level")]
        public string CertificateLavel { get; set; }
        [Display(Name ="Field Of Study")]
        public string FieldofStudy { get; set; }

        [Display(Name="Document")]
        public string Document { get; set; }
        
        public string ImagePath { get; set; }

        public virtual IList<Payroll> Payrolls { get; set; }

        public Employee()
        {
            DateofBirth = DateTime.Now;
            JoiningDate = DateTime.Now;


        }
    }
}