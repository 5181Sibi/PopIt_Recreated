using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopIt.Controllers
{
    public class StudentController : Controller
    {

        private readonly PopItContext _context;

        public StudentController(PopItContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Profile()
        {
            List<StudentDetail> Profile = (from customer in this._context.StudentDetails.Take(10)
                                        select customer).ToList();
            return View(Profile);
        }
        public IActionResult Assessment()
        {
            return View();
        }
        public IActionResult StudyMaterial()
        {
            return View();
        }


    }
}
