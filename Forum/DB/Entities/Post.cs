using System.ComponentModel.DataAnnotations;

namespace Forum.DB.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(10000)]
        public string? Content { get; set; }

        public User Creator { get; set; } //user that created the vote
        public int CreatorId { get; set; } //user id

        public List<PostComment> Comments { get; set; } //List of comments

    }
}
