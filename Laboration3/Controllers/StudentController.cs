using Laboration3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Runtime.Versioning;

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
        public IActionResult AddStudent(Student s, IFormFile file) 
        {
            StudentMethod sm = new StudentMethod();
            int i = 0;
            string error = "";

            //strul här
            String fileName = Upload(file);
            if(fileName != null)
            {
                s.imgPath = fileName;
            }

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

        [HttpGet]
        public IActionResult Search()
        {
            List<Student> studentList = new List<Student>();
            StudentMethod sm = new StudentMethod();
            string error = "";
            studentList = sm.GetStudentsWithReader(out error);
            ViewBag.error = error;
            return View(studentList);
        }


        [HttpPost]
        public IActionResult Search(string input)
        {
            StudentMethod sm = new StudentMethod();
            string error = String.Empty;

            List<Student> students = sm.SearchStudents(input, out string errormsg);

            ViewBag.error = errormsg;

            if(students != null)
            {
                return View(students);
            }
            
            return RedirectToAction("SelectWithDataSet");
        }

        public IActionResult UploadImage()
        {
            return View();
        }


        //Upload Img to DB as bit-array

        //[HttpPost]
        //public async Task<IActionResult> Upload(IFormFile file)
        //{
        //    ImageMethod im = new ImageMethod();

        //    if (file != null && file.Length > 0)
        //    {
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            await file.CopyToAsync(memoryStream);

        //            var contentType = file.ContentType;
        //            var imageData = memoryStream.ToArray();

        //            string errorMessage;
        //            int imageId;

        //            int rowsAffected = im.AddImage(imageData, contentType, out errorMessage);

        //            if (rowsAffected > 0) // Check if an image was successfully added and an imageId was obtained
        //            {
        //                // Redirect to the "Show" action with the imageId - Hårdkodad 1:a atm
        //                return RedirectToAction("Show", new {imageId = 1});
        //            }
        //            else
        //            {
        //                // Handle the error message
        //                // You can choose to show it on the view or log it, for example
        //                ViewBag.ErrorMessage = errorMessage;
        //                return View("SelectWithDataSet");
        //            }
        //        }
        //    }

        //    return View("Index");
        //}

        //public IActionResult Show(int imageId)
        //{
        //    ImageMethod im = new ImageMethod();
        //    string errorMessage;
        //    byte[] imageData = im.GetImageData(imageId, out errorMessage);

        //    if (imageData != null)
        //    {
        //        string contentType = GetContentTypeBasedOnImageData(imageData);

        //        // Pass the image data and content type to the view
        //        ViewData["ImageContent"] = imageData;
        //        ViewData["ImageContentType"] = contentType;

        //        return View();
        //    }
        //    else
        //    {
        //        // Handle the case where no image data is found or an error occurred
        //        // You can choose to show an error message or redirect to another view
        //        ViewBag.ErrorMessage = errorMessage;
        //        return View("Error"); // You can create an "Error" view with the error message
        //    }
        //}

        //private string GetContentTypeBasedOnImageData(byte[] imageData)
        //{
        //    // Check for common image file headers
        //    if (imageData.Length >= 2 && imageData[0] == 0xFF && imageData[1] == 0xD8)
        //    {
        //        return "image/jpeg"; // JPEG header (0xFFD8)
        //    }
        //    else if (imageData.Length >= 8 && imageData[0] == 0x89 && Encoding.ASCII.GetString(imageData, 1, 3) == "PNG")
        //    {
        //        return "image/png"; // PNG header (0x89504E47)
        //    }
        //    else if (imageData.Length >= 6 && Encoding.ASCII.GetString(imageData, 0, 6) == "GIF89a")
        //    {
        //        return "image/gif"; // GIF header ("GIF89a")
        //    }

        //    // If no recognized header is found, you can return a default content type
        //    return "application/octet-stream"; // Default to binary data
        //}

        


        private string Upload(IFormFile file)
        {
            if (file == null || file.Length <= 0) 
            {
                return null;
            }

            var fileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + Path.GetFileName(file.FileName).ToLower();
            //var imagePath = Path.Combine("Image", fileName); // Relative path within the project

            // Save the file to the "Image" folder in your project

            var filePath = Path.Combine("wwwroot", "image", fileName);


            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // Save the image path to the database
            ImageMethod im = new ImageMethod();
            string errorMessage;
            int rowsAffected = im.AddImagePath(fileName, out errorMessage);
            //int rowsAffected = im.AddImagePath(filePath, out errorMessage);

            if (rowsAffected > 0)
            {
                // Image successfully added to the database
                return fileName;
            }
            else
            {
                // Handle the error message
                ViewBag.ErrorMessage = errorMessage;
                return null;
            }

        }


    }
}
