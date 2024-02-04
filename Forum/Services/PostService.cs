using Forum.DB;
using System.Reflection.PortableExecutable;
using Forum.DB.Entities;
using Forum.Models;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using System.Diagnostics;

namespace Forum.Services
{
    public class PostService
    {
        private readonly ForumDbContext db;
        private readonly IMapper mapper;
        public PostService(ForumDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        public int CreatePost(PostModel pModel, int userId)
        {
            var post = mapper.Map<Post>(pModel);
            post.CreatorId = userId;
            post.CreatedDate = DateTime.Now;
            db.Posts.Add(post);
            db.SaveChanges();

            return post.Id;
        }

        public bool ModifyPost(int postId, PostModel pModel, int userId, int roleId)
        {
            var role = (RoleEnum)roleId; 
            var post = db.Posts.FirstOrDefault(p => p.Id == postId);

            if(role == RoleEnum.Admin || post.Creator.Id == userId)
            {
                post.Title = pModel.Title;
                post.Content = pModel.Content;
                db.SaveChanges();
                return true;
            }

            return false;
        }


    }
}
