using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ozzy_mvc.Models
{
    public class Booking 
    {
        public Guid BookingID { get; set; }

        [ForeignKey("Student")]
        public Guid StudentID { get; set; }
        public Guid EquipmentID { get; set; }
        public Equipment Equipment { get; set; }
        public int TimeSlot { get; set; }
        public DateTime Date { get; set; }
    }
}