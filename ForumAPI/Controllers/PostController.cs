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
        /// <param name="author">The author of the posts.</param>
        /// <param name="startDate">The start date of the posts.</param>
        /// <param name="endDate">The end date of the posts.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>A list of post DTOs.</returns>
        [HttpGet]
        public async Task<ActionResult<List<PostDTO>>> Get(string? author = null, DateTime? startDate = null, DateTime? endDate = null, int pageNumber = 1, int pageSize = 10)
        {
            var posts = await _postService.GetAllMessagesAsync(author, startDate, endDate, pageNumber, pageSize);
            if (posts == null)
            {
                return NotFound();
            }
            return posts;
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

        /// <summary>
        /// Creates a new post.
        /// </summary>
        /// <param name="createPostDTO">The post DTO.</param>
        /// <returns>The created post DTO.</returns>
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