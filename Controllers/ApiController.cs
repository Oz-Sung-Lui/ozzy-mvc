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

    }
}
