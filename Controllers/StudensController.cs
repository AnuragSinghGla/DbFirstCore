using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppExercise.Models;

namespace WebAppExercise.Controllers
{
    public class StudensController : Controller
    {
        private readonly StudentDbContext _context;

        public StudensController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: Studens
        public async Task<IActionResult> Index()
        {
            return View(await _context.Studens.ToListAsync());
        }

        // GET: Studens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studens = await _context.Studens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studens == null)
            {
                return NotFound();
            }

            return View(studens);
        }

        // GET: Studens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Studens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age,Course,Email,Contact")] Studens studens)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studens);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studens);
        }

        // GET: Studens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studens = await _context.Studens.FindAsync(id);
            if (studens == null)
            {
                return NotFound();
            }
            return View(studens);
        }

        // POST: Studens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,Course,Email,Contact")] Studens studens)
        {
            if (id != studens.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studens);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudensExists(studens.Id))
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
            return View(studens);
        }

        // GET: Studens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studens = await _context.Studens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studens == null)
            {
                return NotFound();
            }

            return View(studens);
        }

        // POST: Studens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studens = await _context.Studens.FindAsync(id);
            _context.Studens.Remove(studens);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudensExists(int id)
        {
            return _context.Studens.Any(e => e.Id == id);
        }
    }
}
