using AutoMapper;
using Forum.DB;
using Forum.DB.Entities;
using Forum.Exceptions;
using Forum.Models;
using Forum.Services;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Forum.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        /*---------------dependencies----------------*/
        private readonly UserService userService; 
        private readonly ForumDbContext db; //Db Context
        private readonly IMapper mapper; //AutoMapper
        /*-------------------------------------------*/

        public AccountController(UserService userService, ForumDbContext _db, IMapper _mapper)
        {
            this.userService = userService;
            db = _db;
            mapper = _mapper;
        }

        [Route("account/create")]
        [HttpPost]
        public ActionResult CreateAccount(CreateUserModel userModel)
        {
            if (userModel is null)
                throw new NullUserException("User dto can't be null");

            var user = userService.CreateUser(userModel);

            return Ok($"Account ID: {user.Id} created");
        }

        [Route("account/login")]
        [HttpPost]
        public ActionResult Login(CreateUserModel userModel)
        {
            if (userModel is null)
                throw new NullUserException("User dto can't be null");

            var token = userService.Login(userModel);

            if (token is not null)
                return Ok(token);

            throw new Exception("Wrong email or password");
        }

        [Route("user/{userId}")]
        [HttpGet]
        public ActionResult GetUser([FromRoute] int userId)
        {
            var user = db.Users.Include(u => u.Role).Include(u => u.Posts).FirstOrDefault(user => user.Id == userId);

            if (user is null)
                throw new NullUserException("User dto can't be null");

            return Ok(mapper.Map<GetUserModel>(user));

        }

        [Authorize]
        [Route("getAllUsers")]
        [HttpGet]
        public ActionResult GetUsers()
        {
            var users = db.Users.ToList();

            return Ok(users);
        }

    }
}
