using AutoMapper;
using Forum.DB;
using Forum.DB.Entities;
using Forum.Models;
using Isopoh.Cryptography.Argon2;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Forum.Services
{
    public class UserService
    {

        /*---------------dependencies----------------*/
        private readonly ForumDbContext db; //Db Context
        private readonly IMapper mapper; //AutoMapper
        private readonly JwtSettings jwtSettings;
        /*-------------------------------------------*/

        public UserService(ForumDbContext _db, IMapper _mapper, JwtSettings _JwtSettings)
        {
            db = _db;
            mapper = _mapper;
            jwtSettings = _JwtSettings;
        }

        public User CreateUser(CreateUserModel um)
        {
            var user = mapper.Map<User>(um);
            var hashedPassword = Argon2.Hash(user.Password);
            user.Password = hashedPassword;
            user.CreatedDate = DateTime.Now;
            db.Users.Add(user);
            db.SaveChanges();

            return user;
        }

        //todo
        //login method checks if the user sent correct email and password, then sends back jwt token
        public string Login(CreateUserModel um)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == um.Email);
            
            if (user is null)
                return null;

            if(Argon2.Verify(user.Password, um.Password)) //if the password is correct, return token
            {

                List<Claim> claims = new()
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("id", user.Id.ToString()),
                    new Claim("roleId", user.RoleId.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                
                var token = new JwtSecurityToken(jwtSettings.Issuer, jwtSettings.Audience, claims, expires: DateTime.Now.AddDays(30), signingCredentials: creds);


                return new JwtSecurityTokenHandler().WriteToken(token);


            }
            return null;
        }

    }
}
