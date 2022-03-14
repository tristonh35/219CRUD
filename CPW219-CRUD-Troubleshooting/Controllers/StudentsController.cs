﻿using CPW219_CRUD_Troubleshooting.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_CRUD_Troubleshooting.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Student> students = StudentDb.GetStudents(_context);
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(s);
                _context.SaveChanges();
                ViewData["Message"] = $"{s.Name} was added!";
                return View();
            }

            //Show web page with errors
            return View(s);
        }

        public IActionResult Edit(int id)
        {
            //get the student by id
            Student s = StudentDb.GetStudent(_context, id);

            //show it on web page
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Student s)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Update(_context, s);
                ViewData["Message"] = "Product Updated!";
                return View(s);
            }
            //return view with errors
            return View(s);
        }

        public IActionResult Delete(int id)
        {
            Student s = StudentDb.GetStudent(_context, id);
            return View(s);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            //Get Product from database
            Student s = StudentDb.GetStudent(_context, id);

            StudentDb.Delete(_context, s);

            return RedirectToAction("Index");
        }
    }
}
