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
    public class TestRulesController : Controller
    {
        private readonly PopItContext _context;

        public TestRulesController(PopItContext context)
        {
            _context = context;
        }

        // GET: TestRules
        public async Task<IActionResult> Index()
        {
            var popItContext = _context.TestRules.Include(t => t.Test);
            return View(await popItContext.ToListAsync());
        }

        // GET: TestRules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testRule = await _context.TestRules
                .Include(t => t.Test)
                .FirstOrDefaultAsync(m => m.RuleId == id);
            if (testRule == null)
            {
                return NotFound();
            }

            return View(testRule);
        }

        // GET: TestRules/Create
        public IActionResult Create()
        {
            ViewData["TestId"] = new SelectList(_context.TestDetails, "TestId", "TestName");
            return View();
        }

        // POST: TestRules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RuleId,Rules,TotalMark,PassMark,ExamDate,StartTime,EndTime,TestId")] TestRule testRule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testRule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TestId"] = new SelectList(_context.TestDetails, "TestId", "TestName", testRule.TestId);
            return View(testRule);
        }

        // GET: TestRules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testRule = await _context.TestRules.FindAsync(id);
            if (testRule == null)
            {
                return NotFound();
            }
            ViewData["TestId"] = new SelectList(_context.TestDetails, "TestId", "TestName", testRule.TestId);
            return View(testRule);
        }

        // POST: TestRules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RuleId,Rules,TotalMark,PassMark,ExamDate,StartTime,EndTime,TestId")] TestRule testRule)
        {
            if (id != testRule.RuleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testRule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestRuleExists(testRule.RuleId))
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
            ViewData["TestId"] = new SelectList(_context.TestDetails, "TestId", "TestName", testRule.TestId);
            return View(testRule);
        }

        // GET: TestRules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testRule = await _context.TestRules
                .Include(t => t.Test)
                .FirstOrDefaultAsync(m => m.RuleId == id);
            if (testRule == null)
            {
                return NotFound();
            }

            return View(testRule);
        }

        // POST: TestRules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testRule = await _context.TestRules.FindAsync(id);
            _context.TestRules.Remove(testRule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestRuleExists(int id)
        {
            return _context.TestRules.Any(e => e.RuleId == id);
        }
    }
}
