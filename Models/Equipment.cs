using System;
using System.ComponentModel.DataAnnotations;

namespace ozzy_mvc.Models
{
    public class Equipment
    {
        public Guid EquipmentID { get; set; }
        public String Name { get; set; }
        public int RemainAmount { get; set; }
        public int LabNo  { get; set; }
    }
}