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
    public class StudentTeachersController : Controller
    {
        private readonly PopItContext _context;

        public StudentTeachersController(PopItContext context)
        {
            _context = context;
        }

        // GET: StudentTeachers
        public async Task<IActionResult> Index()
        {
            var popItContext = _context.StudentTeachers.Include(s => s.Student).Include(s => s.Subject).Include(s => s.Teacher);
            return View(await popItContext.ToListAsync());
        }

        // GET: StudentTeachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentTeacher = await _context.StudentTeachers
                .Include(s => s.Student)
                .Include(s => s.Subject)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.StudentTeacherId == id);
            if (studentTeacher == null)
            {
                return NotFound();
            }

            return View(studentTeacher);
        }

        // GET: StudentTeachers/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.StudentDetails, "StudentId", "Address1");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
            ViewData["TeacherId"] = new SelectList(_context.TeacherDetails, "TeacherId", "Address1");
            return View();
        }

        // POST: StudentTeachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentTeacherId,SubjectId,TeacherId,StudentId")] StudentTeacher studentTeacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentTeacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.StudentDetails, "StudentId", "Address1", studentTeacher.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", studentTeacher.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.TeacherDetails, "TeacherId", "Address1", studentTeacher.TeacherId);
            return View(studentTeacher);
        }

        // GET: StudentTeachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentTeacher = await _context.StudentTeachers.FindAsync(id);
            if (studentTeacher == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.StudentDetails, "StudentId", "Address1", studentTeacher.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", studentTeacher.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.TeacherDetails, "TeacherId", "Address1", studentTeacher.TeacherId);
            return View(studentTeacher);
        }

        // POST: StudentTeachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentTeacherId,SubjectId,TeacherId,StudentId")] StudentTeacher studentTeacher)
        {
            if (id != studentTeacher.StudentTeacherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentTeacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentTeacherExists(studentTeacher.StudentTeacherId))
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
            ViewData["StudentId"] = new SelectList(_context.StudentDetails, "StudentId", "Address1", studentTeacher.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", studentTeacher.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.TeacherDetails, "TeacherId", "Address1", studentTeacher.TeacherId);
            return View(studentTeacher);
        }

        // GET: StudentTeachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentTeacher = await _context.StudentTeachers
                .Include(s => s.Student)
                .Include(s => s.Subject)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.StudentTeacherId == id);
            if (studentTeacher == null)
            {
                return NotFound();
            }

            return View(studentTeacher);
        }

        // POST: StudentTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentTeacher = await _context.StudentTeachers.FindAsync(id);
            _context.StudentTeachers.Remove(studentTeacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentTeacherExists(int id)
        {
            return _context.StudentTeachers.Any(e => e.StudentTeacherId == id);
        }
    }
}
