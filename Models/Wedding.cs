
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class IsFuture : ValidationAttribute
    {
        protected override ValidationResult IsValid (object value, ValidationContext validationContext)
        {
            if((DateTime)value < DateTime.Now)
                return new ValidationResult("Wedding date must be in the future");
            return ValidationResult.Success;
        }
    }
    public class Wedding
    {
        [Key]
        public int WeddingID {get;set;}
        [Required]
        [Display(Name = "1st Partner's Name")]
        public string PartnerOne {get;set;}
        [Required]
        [Display(Name = "2nd Partner's Name")]
        public string PartnerTwo {get;set;}
        [Required]
        [IsFuture]
        [DataType(DataType.Date)]
        public DateTime Date {get;set;}
        [Required]
        public string Address {get;set;}
        public int UserID {get;set;}
        public User Creator {get;set;}
        public List<RSVP> RSVPs {get;set;}
    }
}