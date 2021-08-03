using Microsoft.AspNetCore.Mvc;
using PopIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopIt.Controllers
{
    public class TeacherController : Controller
    {

        private readonly PopItContext _context;

        public TeacherController(PopItContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
         public IActionResult Profile()
        {
            List<TeacherDetail> Profile = (from customer in this._context.TeacherDetails.Take(10)
                                        select customer).ToList();
            return View(Profile);
        }
        public IActionResult CreateAssessment()
        {
            return View();
        }
        public IActionResult StudyMaterial()
        {
            return View();
        }
    }
}
