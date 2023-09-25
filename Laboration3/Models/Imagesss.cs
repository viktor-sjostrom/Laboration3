using System.ComponentModel.DataAnnotations;

namespace Laboration3.Models
{
    public class Imagesss
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public byte[] Data { get; set; } // VARBINARY(MAX) for image data

        [MaxLength(255)]
        public string ContentType { get; set; } // MIME type of the image

        [Required]
        public int StudentId { get; set; }


    }
}
