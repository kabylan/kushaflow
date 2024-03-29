﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KushaFlow.Models;

using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace KushaFlow.Controllers
{
    public class StudentIsWorksController : Controller
    {
        private readonly KushaFlowContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public StudentIsWorksController(KushaFlowContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }


        // GET: StudentIsWorks
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentIsWork.ToListAsync());
        }
        // GET: StudentIsWorks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentIsWorks/Create
        // загрузка файла.
        [HttpPost]
        public async Task<IActionResult> Create(IFormFile UploadWork, string Title, string Category)
        {
            if (UploadWork != null)
            {
                DateTime dt = DateTime.Now;
                string WorkName = dt.ToString("dd-MM-yyyy") + UploadWork.FileName;

                // path to folder StW
                string WorkPath = "/StW/" + WorkName;

                // save file in folder StW in dir wwwroot
                using (FileStream fileStream = new FileStream(_appEnvironment.WebRootPath + WorkPath, FileMode.Create))
                {
                    await UploadWork.CopyToAsync(fileStream);
                }


                StudentIsWork studentIsWork = new StudentIsWork();

                studentIsWork.Title = Title;
                studentIsWork.Category = Category;
                studentIsWork.WorkName = WorkName;
                studentIsWork.WorkPath = WorkPath;
                studentIsWork.WorkFormat = UploadWork.ContentType;
                studentIsWork.UploadDate = DateTime.Now;

                _context.Add(studentIsWork);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // GET: StudentIsWorks/Download/5
        // скачивание файла
        public async Task<IActionResult> Download(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentIsWork = await _context.StudentIsWork.FindAsync(id);
            if (studentIsWork == null)
            {
                return NotFound();
            }
            string file_path = _appEnvironment.WebRootPath + studentIsWork.WorkPath;

            return PhysicalFile(file_path, studentIsWork.WorkFormat, studentIsWork.WorkName);
        }


        // GET: StudentIsWorks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentIsWork = await _context.StudentIsWork
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentIsWork == null)
            {
                return NotFound();
            }

            string link = "http://kushaflow.ru/StudentIsWorks/Details/";
            ViewBag.LinkToWork = link + studentIsWork.Id;
            return View(studentIsWork);
        }
        private bool StudentIsWorkExists(int id)
        {
            return _context.StudentIsWork.Any(e => e.Id == id);
        }
    }
}
