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
    public class BookingController : Controller
    {
        private readonly OzzyMvcContext _context;

        public BookingController(OzzyMvcContext context)
        {
            _context = context;
        }

        // GET: Booking
        public async Task<IActionResult> Index()
        {
            var ozzyMvcContext = _context.Booking.Include(b => b.Equipment).Include(b => b.Student);
            return View(await ozzyMvcContext.ToListAsync());
        }

        public async Task<IActionResult> ListByEquipmentID(Guid? id)
        {
            var ozzyMvcContext = _context.Booking.Include(b => b.Equipment).Include(b => b.Student);
            var bookingList = await ozzyMvcContext.ToListAsync();
            var newList = bookingList.Where(b => b.EquipmentID == id);

            return View(newList);
        }

        // GET: Booking/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Equipment)
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Booking/Create
        public IActionResult Create(Guid? id, int? eid)
        {
            if (eid != null) ViewData["EquipmentID"] = new SelectList(_context.Equipment.Where(i => (int)i.EquipmentType == eid), "EquipmentID", "EquipmentName");
            else ViewData["EquipmentID"] = new SelectList(_context.Equipment, "EquipmentID", "EquipmentName");
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "Username", id);
            ViewData["TimeSlot"] = new SelectList(Enum.GetValues(typeof(TimeSlot)));
            return View();
        }

        // POST: Booking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingID,StudentID,EquipmentID,TimeSlot,Date")] Booking booking, Guid? id, string su)
        {
            if (ModelState.IsValid)
            {
                booking.BookingID = Guid.NewGuid();
                _context.Add(booking);

                var bookingList = _context.Booking.ToList();
                var properties = new List<string> { "TimeSlot", "Date" };

                foreach (Booking o in bookingList)
                {
                    if (booking.Date == o.Date && booking.TimeSlot == o.TimeSlot
                     && booking.EquipmentID == o.EquipmentID)
                    {
                        ViewData["EquipmentID"] = new SelectList(_context.Equipment, "EquipmentID", "EquipmentName");
                        ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "Username");
                        ViewData["TimeSlot"] = new SelectList(Enum.GetValues(typeof(TimeSlot)));
                        if (su == "admin") return View(booking);
                        else return RedirectToAction("Inventory", "Equipment", new { id = id });
                    }
                }

                await _context.SaveChangesAsync();
                if (su == "admin") return RedirectToAction(nameof(Index));
                else return RedirectToAction("Inventory", "Equipment", new { id = id });
            }
            //ViewData["EquipmentID"] = new SelectList(_context.Equipment, "EquipmentID", "EquipmentID", booking.EquipmentID);
            if (su == "admin") return View(booking);
            else return RedirectToAction("Inventory", "Equipment", new { id = id });
        }

        // GET: Booking/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["Equipment"] = new SelectList(_context.Equipment, "EquipmentID", "EquipmentName", booking.EquipmentID);
            ViewData["Student"] = new SelectList(_context.Student, "StudentID", "Username", booking.StudentID);
            ViewData["TimeSlot"] = new SelectList(Enum.GetValues(typeof(TimeSlot)));
            return View(booking);
        }

        // POST: Booking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("BookingID,StudentID,EquipmentID,TimeSlot,Date")] Booking booking)
        {
            if (id != booking.BookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingID))
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
            ViewData["EquipmentID"] = new SelectList(_context.Equipment, "EquipmentID", "EquipmentID", booking.EquipmentID);
            return View(booking);
        }

        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Equipment)
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var booking = await _context.Booking.FindAsync(id);
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(Guid id)
        {
            return _context.Booking.Any(e => e.BookingID == id);
        }

    }
}
