using System;
using System.ComponentModel.DataAnnotations;

namespace ozzy_mvc.Models
{
    public class Equipment
    {
        public Guid EquipmentID { get; set; }
        public String EquipmentName { get; set; }
        public int LabName  { get; set; }
    }
}