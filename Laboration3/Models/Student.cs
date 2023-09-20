using System.ComponentModel.DataAnnotations;

namespace Laboration3.Models
{
    public class Student
    {

        /*
         * StudentViewModel???
         */

        [Key]
        public int StudentId { get; set; }
        [Required]
        public string FirstName { get; set;}
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set;}


        //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School_Register;Integrated Security=True

    }
}
