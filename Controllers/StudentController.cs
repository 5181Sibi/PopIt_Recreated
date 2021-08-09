using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Polly;
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
        //public IActionResult TestDetails(StudentDetail GradeId)
        //{
        //    List<TestDetail> testDetails = (from customer in this._context.TestDetails.Take(10)
        //                                    select customer).ToList();


        //    //HttpContext.Session.SetInt32("GradeId2", model.GradeId);
        //    //var GradeId = Context.Session.GetInt32("GradeId");
        //    return View(testDetails);
        //}

        public async Task<IActionResult> TestDetails()
        {

            var ap = await (from p in _context.StudentDetails
                            join e in _context.TestDetails on p.GradeId equals e.GradeId
                            
                            select new TestDetail
                            {

                                GradeId = e.GradeId,
                                TestId = e.TestId,
                                TestName = e.TestName,
                                SubjectName = e.SubjectName

                            }).ToListAsync();

            //HttpContext.Session.SetInt32("GradeId", e.GradeId);
            return View(ap);

        }
        public IActionResult TestRules(int TestId)
        {

            List<TestRule> Rules = _context.TestRules.Where(emp => emp.RuleId == TestId).ToList();
            return View(Rules); // but this is not use the obj will return before reached the view

        }
        public IActionResult Questions(int RuleId)
        {

            List<Question> Questions = _context.Questions.Where(emp => emp.QuestionSetNo== RuleId).ToList();
            
            return View(Questions); // but this is not use the obj will return before reached the view

        }
        //[HttpPost]
        //[Route ("submit")]
        //public IActionResult submit(IFormCollection iformcollection)
        //{
        //    string[] QuestionIds = iformcollection["QuestionId"];
        //    foreach(var QuestionId in QuestionIds)
        //    {
        //        int answeridCorrect=
        //    }
        //    return View("Submit");
        //}

        public IActionResult StudyMaterial()
        {
            return View();
        }


    }
}
