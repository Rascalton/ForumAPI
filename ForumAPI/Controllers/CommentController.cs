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

		public CommentsController(CommentService commentService)
		{
			_commentService = commentService;
		}

		[HttpGet("post/{postId}")]
		public async Task<ActionResult<List<CommentDTO>>> GetCommentsByPostId(int postId)
		{
			var comments = await _commentService.GetCommentsByPostIdAsync(postId);
			return Ok(comments);
		}

        [HttpGet("{commentId}")]
		public async Task<ActionResult<List<CommentDTO>>> GetCommentsById(int postId)
		{
			var comments = await _commentService.GetCommentsByIdAsync(postId);
			return Ok(comments);
		}

		[HttpPost, Authorize]
		public async Task<ActionResult<CommentDTO>> CreateComment([FromBody] CreateCommentDTO createCommentDTO)
		{
			try
			{
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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