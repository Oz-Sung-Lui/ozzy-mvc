using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ozzy_mvc.Models;
using ozzy_mvc.Data;

namespace ozzy_mvc.Controllers
{

    public class LabController : Controller
    {

        private readonly OzzyMvcContext _context;

        public LabController(OzzyMvcContext context)
        {
            _context = context;
        }

        public IActionResult ESL()
        {

            var query = from equipment in _context.Set<Equipment>()
                        join booking in _context.Set<Booking>()
                            on equipment.EquipmentID equals booking.EquipmentID
                        select new { equipment, booking };

            EquipmentType[] items = {EquipmentType.OSCILLOSCOPE,
                                    EquipmentType.SIGNAL_GENERATOR,
                                    EquipmentType.MULTIMETER,
                                    EquipmentType.POWER_SUPPLY,
                                    EquipmentType.LOGIC_TRAINER};


            int[] days = { 1, 2, 3, 4, 5, 6 };

            TimeSlot[] slots = {TimeSlot.MORNING,
                                TimeSlot.AFTERNOON,
                                TimeSlot.EVENING};

            var now = DateTime.Now;
            var current_date = now.Date;

            int[,,] results = new int[items.Count(),6,3];

            var i = 0;
            var j = 0;
            var k = 0;

            foreach (EquipmentType item in items)
            {

                var equipmentTemp = _context.Equipment.Where(i => i.EquipmentType == item);

                List<Equipment> eq = equipmentTemp.ToList<Equipment>();

                var fullAmount = eq.Count();

                j=0;
                foreach (int day in days)
                {
                    current_date = current_date.AddDays(day);
                    k=0;
                    foreach (TimeSlot slot in slots)
                    {

                        var data = query.Select(x =>
                            new EquipmentInventory
                            {
                                EquipmentID = x.equipment.EquipmentID,
                                EquipmentName = x.equipment.EquipmentName,
                                EquipmentType = x.equipment.EquipmentType,
                                Description = x.equipment.Description,
                                LabName = x.equipment.LabName,
                                TimeSlot = x.booking.TimeSlot,
                                Date = x.booking.Date,
                                DateStr = String.Format("{0:M/d/yyyy}", x.booking.Date),
                                StudentID = x.booking.StudentID
                            }
                        ).Where(i => i.EquipmentType == item && i.Date == current_date && i.TimeSlot == slot);

                        List<EquipmentInventory> inventory = data.ToList<EquipmentInventory>();

                        results[i,j,k] = fullAmount - inventory.Count();
                        k++;
                    }
                    j++;
                }
                i++;
            }
            
            ViewData["results"] = results;
            
            return View();
        }

        public IActionResult HCRL()
        {
            return View();
        }
        public IActionResult Hardware()
        {
            return View();
        }

        public IActionResult Network()
        {
            return View();
        }

        public IActionResult Robot()
        {
            return View();
        }

    }

}