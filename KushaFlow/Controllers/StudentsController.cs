using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KushaFlow.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace KushaFlow.Controllers
{
    public class StudentsController : Controller
    {
        private readonly KushaFlowContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        public StudentsController(KushaFlowContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET
        public IActionResult AddImage(int id)
        {
            ViewBag.UserId = id;
            return View();
        }

        // POST
        // Upload image
        [HttpPost]
        public async Task<IActionResult> AddImage(int id, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // path to folder StI
                string path = "/StI/" + uploadedFile.FileName;
                // save file in folder StI in dir wwwroot
                using (FileStream fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                var student = await _context.Students.FindAsync(id);

                if (student != null)
                {
                    student.ImgName = uploadedFile.FileName;
                    student.ImgPath = path;
                    _context.Students.Update(student);
                }
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var kushaFlowContext = _context.Students.Include(s => s.Course).Include(s => s.Department).Include(s => s.Institute);
            return View(await kushaFlowContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Course)
                .Include(s => s.Department)
                .Include(s => s.Institute)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Num");
            ViewData["DepartmentId"] = new SelectList(_context.Departament, "Id", "Name");
            ViewData["InstituteId"] = new SelectList(_context.Institute, "Id", "Name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,ImgName,ImgPath,InstituteId,DepartmentId,Group,CourseId,Achievements,InstagramAccount,FacebookAccount")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Num", student.CourseId);
            ViewData["DepartmentId"] = new SelectList(_context.Departament, "Id", "Name", student.DepartmentId);
            ViewData["InstituteId"] = new SelectList(_context.Institute, "Id", "Name", student.InstituteId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            
            ViewBag.UserId = id;

            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Num", student.CourseId);
            ViewData["DepartmentId"] = new SelectList(_context.Departament, "Id", "Name", student.DepartmentId);
            ViewData["InstituteId"] = new SelectList(_context.Institute, "Id", "Name", student.InstituteId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,ImgName,ImgPath,InstituteId,DepartmentId,Group,CourseId,Achievements,InstagramAccount,FacebookAccount")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Num", student.CourseId);
            ViewData["DepartmentId"] = new SelectList(_context.Departament, "Id", "Name", student.DepartmentId);
            ViewData["InstituteId"] = new SelectList(_context.Institute, "Id", "Name", student.InstituteId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Course)
                .Include(s => s.Department)
                .Include(s => s.Institute)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
