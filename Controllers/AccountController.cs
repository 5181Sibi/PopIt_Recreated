using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopIt.Models;
using Scrypt;
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
        EnvironmentVariableTarget count = 0;
        public IActionResult Index()
        {
            return View();
        }



        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ScryptEncoder encoder1= new ScryptEncoder();
                ScryptEncoder encoder = new ScryptEncoder();
                var AdminAccount = await _context.AdminLogins
                .SingleOrDefaultAsync(m => m.AdminNo == model.Username && m.Password == model.Password);


                var StudentAccount = await _context.StudentDetails
                .SingleOrDefaultAsync(m => m.RollNo == model.Username);
                var TeacherAccount = await _context.TeacherDetails
               .SingleOrDefaultAsync(m => m.StaffNo == model.Username);

                 
                if (StudentAccount != null)
                {
                    bool isStudentAccount = encoder1.Compare(model.Password, _context.StudentDetails.SingleOrDefault(x => x.RollNo == model.Username).Password);
                    if (isStudentAccount)
                    {
                        count = 0;
                        HttpContext.Session.SetString("userId", _context.StudentDetails.SingleOrDefault(x => x.RollNo == model.Username).StudentName);
                        HttpContext.Session.SetInt32("GradeId", _context.StudentDetails.SingleOrDefault(x => x.RollNo == model.Username).GradeId);

                        return RedirectToAction("Index", "Student");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Password is wrong.");
                        return View("Index");
                    }

                }
                else if (TeacherAccount !=null)
                {
                    bool IsTeachertAccount = encoder.Compare(model.Password, _context.TeacherDetails.SingleOrDefault(x => x.StaffNo == model.Username).Password);
                    if (IsTeachertAccount)
                    {
                        count = 0;
                        HttpContext.Session.SetString("userId", TeacherAccount.TeacherName);
                        return RedirectToAction("Index", "Teacher");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Password is wrong.");
                        return View("Index");
                    }

                }
                else  if (AdminAccount != null)
                {
                    count = 0;
                    HttpContext.Session.SetString("userId", AdminAccount.AdminNo);
                    return RedirectToAction("Index", "Admin");

                }

                //else 
                //{
                //    count++;
                //    ModelState.AddModelError("Password", "Invalid login attempt.");
                //    return View("Index");
                //}

            }
            else
            {
                ModelState.AddModelError("Password", "Invalid login attempt.");
                return View("Index");
            }

            return View(model);

        }
        [Authorize]
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
