using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ozzy_mvc.Models
{
    public class Student 
    {
        public Guid StudentID { get; set; }

        [Required(ErrorMessage = "*Username is required.")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "*Lenght must between 3 and 15")]
        public String Username { get; set; }

        [Required(ErrorMessage = "*Firstname is required.")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "*Lenght must between 3 and 15")]
        public String Firstname { get; set; }
        
        [Required(ErrorMessage = "*Lastname feild is required.")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Lenght must between 3 and 15")]
        public String Lastname { get; set; }
        
        [Required(ErrorMessage = "*Password is required.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Password must contain at least 8 characters, including UPPER/lowercase and numbers.")]
        public String Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match")]
        public String ConfirmPassword { get; set; }

        public Boolean IsBlacklisted { get; set; }
    }
}