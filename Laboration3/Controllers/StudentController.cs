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

        public IActionResult AddStudent()
        {
            Student student = new Student();
            StudentMethod sm = new StudentMethod();
            int i = 0;
            string error = "";

            student.FirstName = "Alice";
            student.LastName = "Karlsson";
            student.Email = "alka0101@umu.se";

            i = sm.AddStudent(student,out error);
            ViewBag.error = error;
            ViewBag.antal = i;


            return View();
        }
    }
}
