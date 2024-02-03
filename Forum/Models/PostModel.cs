using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class PostModel
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(10000)]
        public string? Content { get; set; }
    }
}
