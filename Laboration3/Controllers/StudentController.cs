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
            if(i == 1) 
            { 
                return RedirectToAction("SelectWithDataSet"); 
            }
            else 
            { 
                return View("AddStudent"); 
            }

        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            StudentMethod sm = new StudentMethod();
            string error = "";
            var student = sm.getStudent(id, out error);
            ViewBag.error = error;
            if (student != null)
            {
                return View(student);
            }

            return RedirectToAction("SelectWithDataSet");
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            StudentMethod sm = new StudentMethod();
            string error = "";
            int i = sm.DeleteStudent(id, out error);
            HttpContext.Session.SetString("antal", i.ToString());
            return RedirectToAction("SelectWithDataSet");

        }

        public IActionResult SelectWithDataSet()
        {
            List<Student> studentList = new List<Student>();
            StudentMethod sm = new StudentMethod();
            string error = "";
            studentList = sm.GetStudentsWithDataSet(out error);
            ViewBag.antal = HttpContext.Session.GetString("antal");
            ViewBag.error = error;
            return View(studentList);
        }

        public IActionResult SelectWithReader()
        {
            List<Student> studentList = new List<Student>();
            StudentMethod sm = new StudentMethod();
            string error = "";
            studentList = sm.GetStudentsWithReader(out error);
            ViewBag.error = error;
            return View(studentList);
        }


        public IActionResult Details(int id) 
        {
            StudentMethod sm = new StudentMethod();
            string error = "";
            var student = sm.getStudent(id, out error);
            ViewBag.error = error;
            if (student != null)
            {
                return View(student);
            }

            return RedirectToAction("SelectWithDataSet");

        }



    }
}
