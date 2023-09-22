using System.ComponentModel.DataAnnotations;


namespace Laboration3.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Required]
        public string CourseName { get; set; }
    }
}
