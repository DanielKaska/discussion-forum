using Forum.DB.Entities;

namespace Forum.Models
{
    public class GetUserModel
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public Role Role { get; set; }
        public List<PostModel> Posts { get; set; }
    }
}
