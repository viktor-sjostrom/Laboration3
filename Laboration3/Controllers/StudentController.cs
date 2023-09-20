using Laboration3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Laboration3.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            
            
            return View();
        }

        [HttpGet]
        public IActionResult AddStudent()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Student s) 
        {
            StudentMethod sm = new StudentMethod();
            int i = 0;
            string error = "";

            i = sm.AddStudent(s, out error);
            ViewBag.error = error;
            ViewBag.antal = i;

            //redirect to listView:n när den är skapad
            if(i == 1) { return RedirectToAction(); }
            else { return View("AddStudent"); }

        }

        public IActionResult SelectWithDataSet()
        {
            List<Student> studentList = new List<Student>();
            StudentMethod sm = new StudentMethod();
            string error = "";
            //studentList = sm.GetStudentsWithDataSet(out error);
            ViewBag.error = error;
            return View(studentList);
        }

    }
}
