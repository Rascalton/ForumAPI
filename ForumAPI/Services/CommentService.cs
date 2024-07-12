using ForumAPI.Data;
using ForumAPI.Models.DTOs;
using ForumAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ForumAPI.Services
{
	public class CommentService
	{
		private readonly DataContext _context;
		
		public CommentService(DataContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Get all comments for a post
		/// </summary>
		/// <param name="postId">The id of the post</param>
		/// <returns>A list of comments</returns>
		public async Task<List<CommentDTO>> GetCommentsByPostIdAsync(int postId)
		{
			return await _context.Comments
				.Where(c => c.ForumPost.Id == postId)
				.Select(c => new CommentDTO
				{
					Id = c.Id,
					Author = c.Author,
					Content = c.Content,
					PostedDate = c.PostedDate
				})
				.ToListAsync();
		}

		/// <summary>
		/// Get a comment by its id
		/// </summary>
		/// <param name="commentId">The id of the comment</param>
		/// <returns>The comment</returns>
		public async Task<CommentDTO> GetCommentsByIdAsync(int commentId)
		{
			var comment = await _context.Comments.FindAsync(commentId);
			if (comment == null)
			{
				throw new ArgumentException("Comment not found");
			}

			return new CommentDTO
			{
				Id = comment.Id,
				Author = comment.Author,
				Content = comment.Content,
				PostedDate = comment.PostedDate
			};
		}

		/// <summary>
		/// Add a comment to a post
		/// </summary>
		/// <param name="postId">The id of the post</param>
		/// <param name="commentDTO">The comment to add</param>
		public async Task<CommentDTO> AddCommentAsync(CreateCommentDTO commentDTO)
		{
			var post = await _context.Posts.FindAsync(commentDTO.ForumPostId);
			if (post == null)
			{
				throw new ArgumentException("Post not found");
			}

			var comment = new CommentEntity
			{
				Author = commentDTO.Author,
				Content = commentDTO.Content,
				PostedDate = DateTime.Now,
				ForumPost = post
			};

			_context.Comments.Add(comment);
			await _context.SaveChangesAsync();

            return new CommentDTO
            {
                Id = comment.Id,
                Author = comment.Author,
                Content = comment.Content,
                PostedDate = comment.PostedDate,
				ForumPostId = comment.ForumPost.Id
            };			
		}

	}
}