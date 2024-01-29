using AutoMapper;
using Forum.DB;
using Forum.DB.Entities;
using Forum.Models;
using Isopoh.Cryptography.Argon2;

namespace Forum.Services
{
    public class UserService
    {

        /*---------------dependencies----------------*/
        private readonly ForumDbContext db; //Db Context
        private readonly IMapper mapper; //AutoMapper
        /*-------------------------------------------*/

        public UserService(ForumDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        public User CreateUser(UserModel um)
        {
            var user = mapper.Map<User>(um);
            var hashedPassword = Argon2.Hash(user.Password);
            user.Password = hashedPassword;

            db.Users.Add(user);
            db.SaveChanges();

            return user;
        }

        //todo
        //login method checks if the user sent correct email and password, then sends back jwt token
        public bool Login(UserModel um)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == um.Email);
            
            if (user is null)
                return false;


            return false;
        }

    }
}
