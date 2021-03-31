using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ozzy_mvc.Models
{
    public class Booking 
    {
        public Guid BookingID { get; set; }

        [ForeignKey("Student")]
        public Guid StudentID { get; set; }

        [ForeignKey("Equipment")]
        public Guid EquipmentID { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Amount { get; set; }
    }
}