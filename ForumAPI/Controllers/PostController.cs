using Microsoft.AspNetCore.Mvc;
using ForumAPI.Services;
using ForumAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;


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
        public async Task<ActionResult<PostDTO>> GetPostById(int id)
        {
            var message = await _postService.GetPostByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }     

        [HttpPost,Authorize]
        public async Task<ActionResult<PostDTO>> CreatePost([FromBody] CreatePostDTO createPostDTO)
        {
            try
            {
                var newMessage = await _postService.AddMessageAsync(createPostDTO);
                return CreatedAtAction(nameof(GetPostById), new { id = newMessage.Id }, newMessage);
            }
            catch 
            {
                
                return StatusCode(500, "An error occurred while creating the message.");
            }
        }          
    }
}