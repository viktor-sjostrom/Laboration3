using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Laboration3.Models
{
    public class Image
    {

        [Key]
        public int ImageId { get; set; }

        public string Title { get; set; }

        [DisplayName("Upload File")]
        public string ImagePath { get; set; }

        public int StudentId { get; set; }
        
        public IFormFile ImageFile { get; set; }
    }
}
