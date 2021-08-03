using Microsoft.AspNetCore.Mvc;
using PopIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopIt.Controllers
{
    public class AdminController : Controller
    {
        private readonly PopItContext _context;

        public AdminController(PopItContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult SubjectTeacher()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult SubjectTeacher(Subject rec)
        {
            _context.Add(rec);
            _context.SaveChanges();
            ViewBag.message = "The Record " + rec.SubjectName + "Is saved Successfully.....!";
            return View(rec);
        }





    }
}
