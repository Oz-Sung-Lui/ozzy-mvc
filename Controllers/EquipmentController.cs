using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ozzy_mvc.Data;
using ozzy_mvc.Models;

namespace ozzy_mvc.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly OzzyMvcContext _context;

        public EquipmentController(OzzyMvcContext context)
        {
            _context = context;
        }

        // GET: Equipment
        public async Task<IActionResult> Index()
        {
            return View(await _context.Equipment.ToListAsync());
        }

        public async Task<IActionResult> Inventory()
        {

            var query = from equipment in _context.Set<Equipment>()
            join booking in _context.Set<Booking>()
                on equipment.EquipmentID equals booking.EquipmentID
            select new { equipment,booking };

            var data = query.Select(x =>
                new EquipmentInventory {
                    EquipmentID = x.equipment.EquipmentID,
                    EquipmentName = x.equipment.EquipmentName,
                    EquipmentType = x.equipment.EquipmentType,
                    Description = x.equipment.Description,
                    LabName = x.equipment.LabName,
                    TimeSlot = x.booking.TimeSlot,
                    Date = x.booking.Date
                }
            );

            List<EquipmentInventory> eq = data.ToList<EquipmentInventory>(); 

            return View(eq);
        }

        // GET: Equipment/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment
                .FirstOrDefaultAsync(m => m.EquipmentID == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // GET: Equipment/Create
        public IActionResult Create()
        {
            ViewData["EquipmentType"] = new SelectList(Enum.GetValues(typeof(EquipmentType)));
            ViewData["LabName"] = new SelectList(Enum.GetValues(typeof(Lab)));
            return View();
        }

        // POST: Equipment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipmentID, EquipmentType, EquipmentName,Description,LabName")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                equipment.EquipmentID = Guid.NewGuid();
                _context.Add(equipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipment);
        }

        // GET: Equipment/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["EquipmentType"] = new SelectList(Enum.GetValues(typeof(EquipmentType)));
            ViewData["LabName"] = new SelectList(Enum.GetValues(typeof(Lab)));

            var equipment = await _context.Equipment.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }
            return View(equipment);
        }

        // POST: Equipment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EquipmentID,EquipmentType,EquipmentName,Description,LabName")] Equipment equipment)
        {
            if (id != equipment.EquipmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentExists(equipment.EquipmentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(equipment);
        }

        // GET: Equipment/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment
                .FirstOrDefaultAsync(m => m.EquipmentID == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // POST: Equipment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var equipment = await _context.Equipment.FindAsync(id);
            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentExists(Guid id)
        {
            return _context.Equipment.Any(e => e.EquipmentID == id);
        }
    }
}
