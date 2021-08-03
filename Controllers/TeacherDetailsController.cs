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
    public class TeacherDetailsController : Controller
    {
        private readonly PopItContext _context;

        public TeacherDetailsController(PopItContext context)
        {
            _context = context;
        }

        // GET: TeacherDetails
        public async Task<IActionResult> Index()
        {
            var popItContext = _context.TeacherDetails.Include(t => t.Category);
            return View(await popItContext.ToListAsync());
        }

        // GET: TeacherDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherDetail = await _context.TeacherDetails
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.TeacherId == id);
            if (teacherDetail == null)
            {
                return NotFound();
            }

            return View(teacherDetail);
        }

        // GET: TeacherDetails/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.UserCategories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: TeacherDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherId,TeacherName,Emailid,StaffNo,Password,ConfirmPassword,CategoryId,SubjectName,Experience,PhoneNumber,Address1,Address2,CreatedOn,Lastlogin,ModifiedOn")] TeacherDetail teacherDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.UserCategories, "CategoryId", "CategoryName", teacherDetail.CategoryId);
            return View(teacherDetail);
        }

        // GET: TeacherDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherDetail = await _context.TeacherDetails.FindAsync(id);
            if (teacherDetail == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.UserCategories, "CategoryId", "CategoryName", teacherDetail.CategoryId);
            return View(teacherDetail);
        }

        // POST: TeacherDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherId,TeacherName,Emailid,StaffNo,Password,ConfirmPassword,CategoryId,SubjectName,Experience,PhoneNumber,Address1,Address2,CreatedOn,Lastlogin,ModifiedOn")] TeacherDetail teacherDetail)
        {
            if (id != teacherDetail.TeacherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherDetailExists(teacherDetail.TeacherId))
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
            ViewData["CategoryId"] = new SelectList(_context.UserCategories, "CategoryId", "CategoryName", teacherDetail.CategoryId);
            return View(teacherDetail);
        }

        // GET: TeacherDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherDetail = await _context.TeacherDetails
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.TeacherId == id);
            if (teacherDetail == null)
            {
                return NotFound();
            }

            return View(teacherDetail);
        }

        // POST: TeacherDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacherDetail = await _context.TeacherDetails.FindAsync(id);
            _context.TeacherDetails.Remove(teacherDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherDetailExists(int id)
        {
            return _context.TeacherDetails.Any(e => e.TeacherId == id);
        }
    }
}
