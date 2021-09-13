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
    public class TestDetailsController : Controller
    {
        private readonly PopItContext _context;

        public TestDetailsController(PopItContext context)
        {
            _context = context;
        }

        // GET: TestDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.TestDetails.ToListAsync());
        }

        // GET: TestDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testDetail = await _context.TestDetails
                .FirstOrDefaultAsync(m => m.TestId == id);
            if (testDetail == null)
            {
                return NotFound();
            }

            return View(testDetail);
        }

        // GET: TestDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TestDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TestId,TestName,SubjectName,GradeId")] TestDetail testDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testDetail);
        }

        // GET: TestDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testDetail = await _context.TestDetails.FindAsync(id);
            if (testDetail == null)
            {
                return NotFound();
            }
            return View(testDetail);
        }

        // POST: TestDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TestId,TestName,SubjectName,GradeId")] TestDetail testDetail)
        {
            if (id != testDetail.TestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestDetailExists(testDetail.TestId))
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
            return View(testDetail);
        }

        // GET: TestDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testDetail = await _context.TestDetails
                .FirstOrDefaultAsync(m => m.TestId == id);
            if (testDetail == null)
            {
                return NotFound();
            }

            return View(testDetail);
        }

        // POST: TestDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testDetail = await _context.TestDetails.FindAsync(id);
            _context.TestDetails.Remove(testDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestDetailExists(int id)
        {
            return _context.TestDetails.Any(e => e.TestId == id);
        }
    }
}
