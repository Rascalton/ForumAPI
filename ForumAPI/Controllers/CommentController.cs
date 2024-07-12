using Microsoft.AspNetCore.Mvc;
using ForumAPI.Services;
using ForumAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ForumAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CommentsController : ControllerBase
	{
		private readonly CommentService _commentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsController"/> class.
        /// </summary>
        /// <param name="commentService">The comment service.</param>
        /// <returns></returns>
		public CommentsController(CommentService commentService)
		{
			_commentService = commentService;
		}

        /// <summary>
        /// Retrieves all comments for a post.
        /// </summary>
        /// <param name="postId">The ID of the post.</param>
        /// <returns>A list of comment DTOs.</returns>
		[HttpGet("post/{postId}")]
		public async Task<ActionResult<List<CommentDTO>>> GetCommentsByPostId(int postId)
		{
			var comments = await _commentService.GetCommentsByPostIdAsync(postId);
			return Ok(comments);
		}

        /// <summary>
        /// Retrieves a comment by its ID.
        /// </summary>
        /// <param name="commentId">The ID of the comment.</param>
        /// <returns>The comment DTO.</returns>
        [HttpGet("{commentId}")]
		public async Task<ActionResult<List<CommentDTO>>> GetCommentsById(int postId)
		{
			var comments = await _commentService.GetCommentsByIdAsync(postId);
			return Ok(comments);
		}

        /// <summary>
        /// Creates a new comment.
        /// </summary>
        /// <param name="createCommentDTO">The comment DTO.</param>
        /// <returns>The created comment DTO.</returns>
		[HttpPost, Authorize]
		public async Task<ActionResult<CommentDTO>> CreateComment([FromBody] CreateCommentDTO createCommentDTO)
		{
			try
			{
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                if (userId == null)
                {
                    return Unauthorized();
                }

                createCommentDTO.Author = userId;
				CommentDTO newComment = await _commentService.AddCommentAsync(createCommentDTO);
                
				return CreatedAtAction(nameof(GetCommentsByPostId), new { messageId = newComment.Id }, newComment);
			}
			catch
			{
				return StatusCode(500, "An error occurred while creating the comment.");
			}
		}
	}
}