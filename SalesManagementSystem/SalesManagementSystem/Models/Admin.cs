using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SalesManagementSystem.Models
{
    public class Admin
    {
        [Display(Name = "Enter Userame")]
        public string aid { get; set; }

        [Required]
        [Display(Name = "Enter Your Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Age")]
        [Range(20, 40)]
        public int age { get; set; }

        [Required(ErrorMessage = "Email Required!")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [Display(Name = "Enter Password")]
        [MinLength(8), MaxLength(8)]
        public string password { get; set; }
    }
}