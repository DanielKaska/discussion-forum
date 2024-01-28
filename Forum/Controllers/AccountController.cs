using AutoMapper;
using Forum.DB;
using Forum.DB.Entities;
using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Mvc;

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

        [Route("create")]
        [HttpPost]
        public ActionResult CreateAccount(UserModel userModel)
        {
            if (userModel is null)
                return BadRequest("user is null");

            var user = mapper.Map<User>(userModel);

            db.Users.Add(user);
            db.SaveChanges();
            return Ok($"Account ID: {user.Id} created");
        }

        [Route("getAllUsers")]
        [HttpGet]
        public ActionResult GetUsers()
        {
            var users = db.Users.ToList();

            return Ok(users);
        }

    }
}
