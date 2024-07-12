using Microsoft.AspNetCore.Mvc;
using ForumAPI.Services;
using ForumAPI.Models.DTOs;


namespace ForumAPI.Controllers
{
    /// <summary>
    /// Controller for managing posts.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly PostService _postService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostsController"/> class.
        /// </summary>
        /// <param name="postService">The post service.</param>
        public PostsController(PostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// Retrieves all posts.
        /// </summary>
        /// <returns>A list of post DTOs.</returns>
        /// [HttpGet]
        public async Task<ActionResult<List<PostDTO>>> Get()
        {
            var messages = await _postService.GetAllMessagesAsync();
            return Ok(messages);
        }
       
    }
}