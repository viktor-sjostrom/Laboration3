using Laboration3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

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

        [HttpGet]
        public IActionResult Edit(int id)
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

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student s) 
        {
            StudentMethod sm = new StudentMethod();
            string error = "";

            int i = sm.Update(s, out error);

            ViewBag.error = error;
            return RedirectToAction("SelectWithDataSet");
        }

        [HttpGet]
        public IActionResult Filtrering()
        {
            StudentCourseMethod scm = new StudentCourseMethod();
            CourseMethod cm = new CourseMethod();

            ViewModelRegistrationCourse myModel = new ViewModelRegistrationCourse
            {
                StudentCourseList = scm.GetStudentCourse(out string errormsg),
                CourseList = cm.GetCourseList(out string errormsg2)
            };
            ViewBag.error = @"1: {errormsg} 2: {errormsg2}";

            return View(myModel);
        }

        [HttpGet]
        public IActionResult Filtrering2()
        {
            StudentCourseMethod scm = new StudentCourseMethod();
            CourseMethod cm = new CourseMethod();

            ViewModelRegistrationCourse myModel = new ViewModelRegistrationCourse
            {
                StudentCourseList = scm.GetStudentCourse(out string errormsg),
                CourseList = cm.GetCourseList(out string errormsg2)
            };

            List<Course> courseList = new List<Course>();
            courseList = cm.GetCourseList(out string errormsg3);

            //De går att skicka med en viewdata
            ViewBag.error = @"1: {errormsg} 2: {errormsg2} 3: {errormsg3}";
            ViewData["courseList"] = courseList;

            //det går att skicka m en viewbag
            ViewBag.courseList = courseList;

            return View(myModel);
        }

        [HttpPost]
        public IActionResult Filtrering2(string course)
        {
            int i = Convert.ToInt32(course);
            ViewData["Course"] = i;

            StudentCourseMethod scm = new StudentCourseMethod();
            CourseMethod cm = new CourseMethod();

            ViewModelRegistrationCourse myModel = new ViewModelRegistrationCourse
            {
                StudentCourseList = scm.GetStudentCourse(out string errormsg),
                CourseList = cm.GetCourseList(out string errormsg2)
            };

            List<Course> courseList = new List<Course>();
            courseList = cm.GetCourseList(out string errormsg3);

            //De går att skicka med en viewdata
            ViewBag.error = @"1: {errormsg} 2: {errormsg2} 3: {errormsg3}";
            ViewData["courseList"] = courseList;

            ViewBag.courseList = courseList;
            ViewBag.message = course;

            return View(myModel);
        }

        [HttpGet]
        public IActionResult Filtrering3()
        {
            StudentCourseMethod scm = new StudentCourseMethod();
            CourseMethod cm = new CourseMethod();

            ViewModelRegistrationCourse myModel = new ViewModelRegistrationCourse
            {
                StudentCourseList = scm.GetStudentCourse(out string errormsg),
                CourseList = cm.GetCourseList(out string errormsg2)
            };

            List<Course> courseList = new List<Course>();
            courseList = cm.GetCourseList(out string errormsg3);

            //De går att skicka med en viewdata
            ViewBag.error = @"1: {errormsg} 2: {errormsg2} 3: {errormsg3}";
            ViewData["courseList"] = courseList;

            //det går att skicka m en viewbag
            ViewBag.courseList = courseList;

            return View(myModel);
        }

        [HttpPost]
        public IActionResult Filtrering3(string course)
        {
            int i = Convert.ToInt32(course);
            
            StudentCourseMethod scm = new StudentCourseMethod();
            CourseMethod cm = new CourseMethod();

            ViewModelRegistrationCourse myModel = new ViewModelRegistrationCourse
            {
                StudentCourseList = scm.GetStudentCourses(out string errormsg, i),
                CourseList = cm.GetCourseList(out string errormsg2)
            };

            List<Course> courseList = new List<Course>();
            courseList = cm.GetCourseList(out string errormsg3);

            //De går att skicka med en viewdata
            ViewBag.error = @"1: {errormsg} 2: {errormsg2} 3: {errormsg3}";
            ViewData["courseList"] = courseList;

            ViewBag.courseList = courseList;
            ViewBag.message = course;
            ViewData["Course"] = i;

            return View(myModel);
        }

        [HttpGet]
        public IActionResult Sortering(string sortering)
        {
            StudentCourseMethod scm = new StudentCourseMethod();
            CourseMethod cm = new CourseMethod();

            List<StudentCourse> studentCourseList = scm.GetStudentCourse(out string errormsg);

            // Determine the sorting direction based on the current state
            string currentSortDirection = HttpContext.Session.GetString("SortDirection");

            // Default sorting is ascending
            bool isAscending = true;

            if (currentSortDirection != null)
            {
                isAscending = currentSortDirection == "asc";
            }


            // Pass the sorting direction to the view
            ViewBag.SortDirection = isAscending ? "asc" : "desc";

            // Sort the student course list based on the sortering parameter and direction
            if (sortering == "firstname")
            {
                if (isAscending)
                {
                    studentCourseList = studentCourseList.OrderBy(s => s.FirstName).ToList();
                    HttpContext.Session.SetString("SortDirection", "desc");
                }
                else
                {
                    studentCourseList = studentCourseList.OrderByDescending(s => s.FirstName).ToList();
                    HttpContext.Session.SetString("SortDirection", "asc");
                }
            }
            else
            {
                // Default sorting or handle other sorting options
                // You can add more sorting options here if needed
            }


            ViewModelRegistrationCourse myModel = new ViewModelRegistrationCourse
            {
                StudentCourseList = studentCourseList,
                //StudentCourseList = scm.GetStudentCourse(out string errormsg1),
                CourseList = cm.GetCourseList(out string errormsg2)
            };

            ViewBag.sortera = sortering;
            return View(myModel);
        }

    }
}
