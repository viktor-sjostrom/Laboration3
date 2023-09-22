namespace Laboration3.Models
{
    public class ViewModelRegistrationCourse
    {
        public IEnumerable<Course> CourseList { get;set; }
        public IEnumerable<StudentCourse> StudentCourseList { get;set; }
    }
}
