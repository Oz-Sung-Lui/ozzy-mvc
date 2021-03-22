using System;

namespace ozzy_mvc.Models
{
    public class Student 
    {
        public Guid StudentID { get; set; }
        public String Username { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public String Password { get; set; }
        public Boolean isBlacklisted {get; set; }
    }
}