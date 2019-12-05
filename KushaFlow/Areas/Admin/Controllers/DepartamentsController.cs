using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KushaFlow.Models;

namespace KushaFlow.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartamentsController : Controller
    {
        private readonly KushaFlowContext _context;

        public DepartamentsController(KushaFlowContext context)
        {
            _context = context;
        }

        // GET: Admin/Departaments
        public async Task<IActionResult> Index()
        {
            var kushaFlowContext = _context.Departament.Include(d => d.Institut);
            return View(await kushaFlowContext.ToListAsync());
        }

        // GET: Admin/Departaments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departament = await _context.Departament
                .Include(d => d.Institut)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departament == null)
            {
                return NotFound();
            }

            return View(departament);
        }

        // GET: Admin/Departaments/Create
        public IActionResult Create()
        {
            ViewData["InstitutId"] = new SelectList(_context.Institute, "Id", "Name");
            return View();
        }

        // POST: Admin/Departaments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,InstitutId")] Departament departament)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstitutId"] = new SelectList(_context.Institute, "Id", "Name", departament.InstitutId);
            return View(departament);
        }

        // GET: Admin/Departaments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departament = await _context.Departament.FindAsync(id);
            if (departament == null)
            {
                return NotFound();
            }
            ViewData["InstitutId"] = new SelectList(_context.Institute, "Id", "Name", departament.InstitutId);
            return View(departament);
        }

        // POST: Admin/Departaments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,InstitutId")] Departament departament)
        {
            if (id != departament.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentExists(departament.Id))
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
            ViewData["InstitutId"] = new SelectList(_context.Institute, "Id", "Name", departament.InstitutId);
            return View(departament);
        }

        // GET: Admin/Departaments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departament = await _context.Departament
                .Include(d => d.Institut)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departament == null)
            {
                return NotFound();
            }

            return View(departament);
        }

        // POST: Admin/Departaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departament = await _context.Departament.FindAsync(id);
            _context.Departament.Remove(departament);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartamentExists(int id)
        {
            return _context.Departament.Any(e => e.Id == id);
        }
    }
}
