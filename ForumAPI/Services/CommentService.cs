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
	}
}