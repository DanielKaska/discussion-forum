using AutoMapper;
using Forum.DB.Entities;

namespace Forum.Models
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<UserModel, User>();
        }
    }
}
