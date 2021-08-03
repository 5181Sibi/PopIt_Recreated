using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PopIt.Models;

namespace PopIt.Controllers
{
    public class StudentDetailsController : Controller
    {
        private readonly PopItContext _context;

        public StudentDetailsController(PopItContext context)
        {
            _context = context;
        }

        // GET: StudentDetails
        public async Task<IActionResult> Index()
        {
            var popItContext = _context.StudentDetails.Include(s => s.Category);
            return View(await popItContext.ToListAsync());
        }

        // GET: StudentDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentDetail = await _context.StudentDetails
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (studentDetail == null)
            {
                return NotFound();
            }

            return View(studentDetail);
        }

        // GET: StudentDetails/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.UserCategories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: StudentDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,Emailid,RollNo,Password,ConfirmPassword,CategoryId,GradeId,StudentName,Dob,Address1,Address2,PhoneNumber,CreatedOn,Lastlogin,ModifiedOn")] StudentDetail studentDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.UserCategories, "CategoryId", "CategoryName", studentDetail.CategoryId);
            return View(studentDetail);
        }

        // GET: StudentDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentDetail = await _context.StudentDetails.FindAsync(id);
            if (studentDetail == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.UserCategories, "CategoryId", "CategoryName", studentDetail.CategoryId);
            return View(studentDetail);
        }

        // POST: StudentDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,Emailid,RollNo,Password,ConfirmPassword,CategoryId,GradeId,StudentName,Dob,Address1,Address2,PhoneNumber,CreatedOn,Lastlogin,ModifiedOn")] StudentDetail studentDetail)
        {
            if (id != studentDetail.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentDetailExists(studentDetail.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.UserCategories, "CategoryId", "CategoryName", studentDetail.CategoryId);
            return View(studentDetail);
        }

        // GET: StudentDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentDetail = await _context.StudentDetails
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (studentDetail == null)
            {
                return NotFound();
            }

            return View(studentDetail);
        }

        // POST: StudentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentDetail = await _context.StudentDetails.FindAsync(id);
            _context.StudentDetails.Remove(studentDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentDetailExists(int id)
        {
            return _context.StudentDetails.Any(e => e.StudentId == id);
        }
    }
}
