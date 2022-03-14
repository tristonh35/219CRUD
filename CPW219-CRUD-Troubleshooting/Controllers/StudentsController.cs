using CPW219_CRUD_Troubleshooting.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_CRUD_Troubleshooting.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext context;

        public StudentsController(SchoolContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Student> students = StudentDb.GetStudents(context);
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
                StudentDb.Add(s, context);
                ViewData["Message"] = $"{s.Name} was added!";
                return View();
            }

            //Show web page with errors
            return View(s);
        }

        public IActionResult Edit(int id)
        {
            //get the student by id
            Student s = StudentDb.GetStudent(context, id);

            //show it on web page
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Student s)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Update(context, s);
                ViewData["Message"] = "Product Updated!";
                return View(s);
            }
            //return view with errors
            return View(s);
        }

        public IActionResult Delete(int id)
        {
            Student s = StudentDb.GetStudent(context, id);
            return View(s);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            //Get Product from database
            Student s = StudentDb.GetStudent(context, id);

            StudentDb.Delete(context, s);

            return RedirectToAction("Index");
        }
    }
}
