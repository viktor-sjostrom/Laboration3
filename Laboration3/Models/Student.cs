using System.ComponentModel.DataAnnotations;

namespace Laboration3.Models
{
    public class Student
    {

        /*
         * Added validation to Email, and custom error msg if wrong
         */

        [Key]
        public int StudentId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide A First Name")]
        public string FirstName { get; set;}
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide A Last Name")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide A Email")]
        [EmailAddress(ErrorMessage = "Please Provide A Valid Email Adress")]
        public string Email { get; set;}

        public string? imgPath { get; set;}

    }
}
