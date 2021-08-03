using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopIt.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PopIt.Controllers
{
    public class AccountController : Controller
    {
        private readonly PopItContext _context;

        public AccountController(PopItContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }



        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var AdminAccount = await _context.AdminLogins
                .SingleOrDefaultAsync(m => m.AdminNo == model.Username && m.Password == model.Password);
                var StudentAccount = await _context.StudentDetails
                .SingleOrDefaultAsync(m => m.RollNo == model.Username && m.Password == model.Password);
                var TeacherAccount = await _context.TeacherDetails
               .SingleOrDefaultAsync(m => m.StaffNo == model.Username && m.Password == model.Password);

                if (AdminAccount != null)
                {
                    HttpContext.Session.SetString("userId", AdminAccount.AdminNo);
                    return RedirectToAction("Index", "Admin");

                }
                else if (StudentAccount != null)
                {
                    HttpContext.Session.SetString("userId", StudentAccount.StudentName);
                    return RedirectToAction("Index", "Student");

                }
                else if (TeacherAccount != null)
                {
                    HttpContext.Session.SetString("userId", TeacherAccount.TeacherName);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    return View("Index");
                }


            }
            
            return View();

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
        public void ValidationMessage(string key, string alert, string value)
        {
            try
            {
                TempData.Remove(key);
                TempData.Add(key, value);
                TempData.Add("alertType", alert);
            }
            catch
            {
                Debug.WriteLine("TempDataMessage Error");
            }

        }
    }
}
