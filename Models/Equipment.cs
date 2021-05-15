using System;

namespace ozzy_mvc.Models
{
    public class Equipment
    {
        public Guid EquipmentID { get; set; }
        public String EquipmentName { get; set; }
        public EquipmentType EquipmentType { get; set; }
        public String Description { get; set; }
        public Lab LabName  { get; set; }
    }

    public class EquipmentInventory
    {
        public Guid EquipmentID { get; set; }
        public String EquipmentName { get; set; }
        public EquipmentType EquipmentType { get; set; }
        public String Description { get; set; }
        public Lab LabName  { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public DateTime Date { get; set; }
    }

    public enum Lab {
        HARDWARE,
        HCRL,
        ROBOT_IOT,
        ESL,
        NETWORK,
    }
    
    public enum EquipmentType {
        ELECTRIC_DRILL_SET,
        SCREWDRIVER_SET,
        SOLDERING_IRON,
        HAMMER,
        HACKSAW,
        PLIERS,
        CROWBAR,
        WRENCH,
        MONITOR,
        PROJECTOR,
        ARDUINO,
        NODEMCU_3,
        RASPBERRY_PI_4,
        OSCILLOSCOPE,
        SIGNAL_GENERATOR,
        MULTIMETER,
        POWER_SUPPLY,
        LOGIC_TRAINER,
        LAN_CAT6_CABLE,
        RJ45_CRIMPING_TOOL,
        ROUTER,
        SWITCH,
    }
}