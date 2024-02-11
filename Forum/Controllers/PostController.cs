using AutoMapper;
using Forum.DB;
using Forum.Exceptions;
using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
                throw new NullPostException("Post model cant be null");

            var userIdClaim = Request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");

            if(userIdClaim is not null)
            {
                var postId = postService.CreatePost(pModel, int.Parse(userIdClaim.Value));
                return Ok(postId);
            }

            return BadRequest("Unexpected error happened");
            
        }

        [Authorize]
        [HttpPost("post/modify/{postId}")]
        public ActionResult ModifyPost([FromRoute] int postId, [FromBody] PostModel pModel)
        {
            var userIdClaim = Request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
            var userRoleClaim = Request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "roleId");

            if (userIdClaim is null || userRoleClaim is null)
                throw new Exception("Unexpected token error");

            var modified = postService.ModifyPost(postId, pModel, int.Parse(userIdClaim.Value), int.Parse(userRoleClaim.Value));

            if (modified)
                return Ok();
            
            return BadRequest("Unexpected error happened");
        }

        [HttpGet("post/get/")]
        public ActionResult GetPostsByQuery([FromQuery] string query)
        {
            return Ok(postService.GetPost(query));
        }

        [Authorize]
        [HttpPost("post/addComment")]
        public ActionResult AddComment([FromBody] AddCommentModel commentModel)
        {
            var userId = int.Parse(Request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value);

            var created = postService.AddComment(commentModel, userId);

            if(created) return Ok();

            throw new Exception("Unexpected error happened");
        }

    }
}
