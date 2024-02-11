using AutoMapper;
using Forum.DB.Entities;

namespace Forum.Models
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<UserModel, User>(); //convert model sent when creating an account
            CreateMap<User, GetUserModel>();

            CreateMap<PostModel, Post>(); //convert model sent when creating a post 
            CreateMap<Post, GetPostModel>(); //convert from post to post model

            CreateMap<Post, PostModel>();
            


        }
    }
}
