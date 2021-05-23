using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using ozzy_mvc.Data;
using ozzy_mvc.Models;

namespace ozzy_mvc.Controllers
{
    [Controller]
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly OzzyMvcContext _context;

        public ApiController(OzzyMvcContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("equipment")]
        public ActionResult<List<Equipment>> GetAllEquipments()
        {
            return _context.Equipment.ToList<Equipment>();
        }

        [HttpGet]
        [Route("student")]
        public ActionResult<List<Student>> GetAllStudents()
        {
            return _context.Student.ToList<Student>();
        }

        [HttpGet]
        [Route("booking")]
        public ActionResult<List<Booking>> GetAllBookings()
        {
            return _context.Booking.ToList<Booking>();
        }

        [HttpGet("booking/{guid}")]
        [Route("booking")]
        public ActionResult<List<Booking>> GetMyBookings(Guid guid)
        {
            return _context.Booking.Where(i => i.StudentID == guid).ToList<Booking>();
        }

        [HttpPost]
        [Route("booking")]
        public async Task<IActionResult> CreateNewBooking([FromForm] Booking booking)
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
                    && booking.StudentID == o.StudentID && booking.EquipmentID == o.EquipmentID)
                    {
                        return Json(new {message="Unable to booking"});
                    }
                }

                await _context.SaveChangesAsync();
                return Json(new {message="Created"});
            }
            return Json(new {message="Unable to booking"});
        }

    }
}
