using System.ComponentModel.DataAnnotations;

namespace Forum.DB.Entities
{
    public class PostComment
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }
        
        public virtual Post Post { get; set; }
        public int PostId { get; set; }
        

        public DateTime CommentCreatedDate { get; set; }

    }
}
