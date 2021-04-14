using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ozzy_mvc.Models
{
    public class Equipment
    {
        public Guid EquipmentID { get; set; }
        public String EquipmentName { get; set; }
        public String LabName  { get; set; }
        public Booking Booking { get; set; }
    }
}