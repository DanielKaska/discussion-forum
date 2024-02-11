using Forum.DB.Entities;

namespace Forum.Models
{
    public class GetPostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedDate { get; set; }

        public List<AddCommentModel> Comments { get; set; }

    }
}
