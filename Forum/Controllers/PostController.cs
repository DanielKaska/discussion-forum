using AutoMapper;
using Forum.DB;
using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{

    [ApiController]
    public class PostController : Controller
    {

        /*---------------dependencies----------------*/
        private readonly PostService postService;
        /*-------------------------------------------*/

        public PostController(PostService postService)
        {
            this.postService = postService;
        }

        [Authorize]
        [HttpPost("post/create")]
        public ActionResult CreatePost([FromBody] PostModel pModel)
        {
            if (pModel is null)
                return BadRequest("Post model cant be null");

            var userIdClaim = Request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");

            if(userIdClaim is not null)
            {
                var postId = postService.CreatePost(pModel, int.Parse(userIdClaim.Value));
                return Ok(postId);
            }

            return BadRequest();
            
        }

        [Authorize]
        [HttpPost("post/modify/{postId}")]
        public ActionResult ModifyPost([FromRoute] int postId, [FromBody] PostModel pModel)
        {
            var userIdClaim = Request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
            var userRoleClaim = Request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "roleId");

            if(userIdClaim is null || userRoleClaim is null)
                BadRequest("Token error");

            var modified = postService.ModifyPost(postId, pModel, int.Parse(userIdClaim.Value), int.Parse(userRoleClaim.Value));

            if (modified)
                return Ok();
            
            return BadRequest();
        }


    }
}
