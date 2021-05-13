using System;
using System.ComponentModel;

namespace ozzy_mvc.Models
{
    public class Booking 
    {
        public Guid BookingID { get; set; }
        public Guid StudentID { get; set; }
        public Student Student { get; set; }
        public Guid EquipmentID { get; set; }
        public Equipment Equipment { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public DateTime Date { get; set; }
    }

     public enum TimeSlot {
        MORNING,
        AFTERNOON,
        EVENING
    }
}