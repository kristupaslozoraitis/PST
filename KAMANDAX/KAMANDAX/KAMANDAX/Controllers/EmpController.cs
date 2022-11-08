using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KAMANDAX.Models;
using Microsoft.AspNetCore.Mvc;

namespace KAMANDAX.Controllers
{
    public class EmpController : Controller
    {
        public IActionResult Index()
        {
            Product db = new Product();

            return View(db.Title.ToList());
        }
    }
}