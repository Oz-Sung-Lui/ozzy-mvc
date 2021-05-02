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
    public class AuthController : Controller
    {
        private readonly OzzyMvcContext _context;

        public AuthController(OzzyMvcContext context)
        {
            _context = context;
        }

        // GET: Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Auth/Login
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Username")] string Username, [Bind("Password")] string Password)
        {

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Username == Username);
            if (student == null)
            {
                return NotFound();
            }

            if (student.Password != Password) {
                return View();
            }

            CookieOptions cookieOptions = new CookieOptions();            
            HttpContext.Response.Cookies.Append("CurrentID", student.StudentID.ToString(), cookieOptions);
            HttpContext.Response.Cookies.Append("CurrentUsername", student.Username, cookieOptions);

            return RedirectToAction("Index", "Home");
        }

    }
}
