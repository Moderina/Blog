using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int PostID { get; set; }
        public string UserID { get; set; }
        public DateTime Date { get; set; }

        [Required]
        public string Text { get; set; }

        public string AddressIP { get; set; }

    }
}
