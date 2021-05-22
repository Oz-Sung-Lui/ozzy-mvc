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
    public class StudentController : Controller
    {
        private readonly OzzyMvcContext _context;

        public StudentController(OzzyMvcContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            return View(await _context.Student.ToListAsync());
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,Username,Firstname,Lastname,Password,ConfirmPassword")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.StudentID = Guid.NewGuid();
                student.IsBlacklisted = false;
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StudentID,Username,Firstname,Lastname,Password,ConfirmPassword")] Student student)
        {
            if (id != student.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentID))
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
            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(Guid id)
        {
            return _context.Student.Any(e => e.StudentID == id);
        }
        public async Task<IActionResult> Cancel(Guid id)
        {

            var query = from equipment in _context.Set<Equipment>()
            join booking in _context.Set<Booking>()
                on equipment.EquipmentID equals booking.EquipmentID
            select new { equipment,booking };

            var data = query.Select(x =>
                new EquipmentInventory { EquipmentID = x.equipment.EquipmentID,
                    EquipmentName = x.equipment.EquipmentName,
                    EquipmentType = x.equipment.EquipmentType,
                    Description = x.equipment.Description,
                    LabName = x.equipment.LabName,
                    TimeSlot = x.booking.TimeSlot,
                    Date = x.booking.Date,
                    DateStr = String.Format("{0:M/d/yyyy}", x.booking.Date),
                    StudentID = x.booking.StudentID,
                    BookingID = x.booking.BookingID
                }
            ).Where(i => i.StudentID == id && i.Date >= DateTime.Now).OrderBy(i => i.EquipmentName).ThenBy(i => i.Date);
            
            List<EquipmentInventory> eq = data.ToList<EquipmentInventory>(); 

            return View(eq);
        }

        public async Task<IActionResult> Return(Guid id)
        {

            var query = from equipment in _context.Set<Equipment>()
            join booking in _context.Set<Booking>()
                on equipment.EquipmentID equals booking.EquipmentID
            select new { equipment,booking };


            var data = query.Select(x =>
                new EquipmentInventory { EquipmentID = x.equipment.EquipmentID,
                    EquipmentName = x.equipment.EquipmentName,
                    EquipmentType = x.equipment.EquipmentType,
                    Description = x.equipment.Description,
                    LabName = x.equipment.LabName,
                    TimeSlot = x.booking.TimeSlot,
                    Date = x.booking.Date,
                    DateStr = String.Format("{0:M/d/yyyy}", x.booking.Date),
                    StudentID = x.booking.StudentID,
                    BookingID = x.booking.BookingID
                }
            ).Where(i => i.StudentID == id && i.Date < DateTime.Now).OrderBy(i => i.EquipmentName).ThenBy(i => i.Date);
            
            List<EquipmentInventory> eq = data.ToList<EquipmentInventory>(); 

            return View(eq);
        }

        public async Task<IActionResult> DeleteReturn(Guid id)
        {
            var booking = await _context.Booking.FindAsync(id);
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Return), new {id = booking.StudentID});
        }
        
        public async Task<IActionResult> DeleteCancel(Guid id)
        {
            var booking = await _context.Booking.FindAsync(id);
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Cancel), new {id = booking.StudentID});
        }
    }
}
