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
        [HttpGet]
        public async Task<ActionResult<List<PostDTO>>> Get()
        {
            var messages = await _postService.GetAllMessagesAsync();
            return Ok(messages);
        }

        /// <summary>
        /// Retrieves a post by its ID.
        /// </summary>
        /// <param name="id">The ID of the post.</param>
        /// <returns>The post DTO.</returns>
        /// <response code="200">Returns the post DTO.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetMessageById(int id)
        {
            var message = await _postService.GetPostByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }       
    }
}