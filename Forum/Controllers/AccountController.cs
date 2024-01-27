using AutoMapper;
using Forum.DB;
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
        private readonly ForumDbContext db;
        private readonly IMapper mapper;
        /*-------------------------------------------*/

        public AccountController(UserService userService, ForumDbContext _db, IMapper _mapper)
        {
            this.userService = userService;
            db = _db;
            mapper = _mapper;
        }


        [HttpPost]
        public ActionResult CreateAccount(UserModel user)
        {
            if (user is null)
                return BadRequest("user is null");




            return BadRequest();
        }
    }
}
