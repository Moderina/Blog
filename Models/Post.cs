using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Reflection.Metadata;

namespace Blog.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }
        public string Author { get; set; }

        public DateTime date { get; set; }

        [NotMapped]
        public FormFile? Image { get; set; }

        public string SerializedImages { get; set; }

        public string? Tags { get; set; }

        public string? Commends { get; set; }
    }
}
