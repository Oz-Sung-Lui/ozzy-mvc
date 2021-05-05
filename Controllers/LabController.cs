using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ozzy_mvc.Models;

namespace ozzy_mvc.Controllers
{
    public class LabController : Controller
    {
        public IActionResult Lab()
        {
            return View();
        }
    }

}