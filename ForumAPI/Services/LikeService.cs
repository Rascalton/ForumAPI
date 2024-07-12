using ForumAPI.Data;
using ForumAPI.Models.DTOs;
using ForumAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAPI.Services
{
	public class LikeService
	{
		private readonly DataContext _context;

		public LikeService(DataContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task ToggleLikeAsync(ToggleLikeDTO toggleLikeDTO)
		{
			var like = await _context.Likes
				.FirstOrDefaultAsync(l => l.User == toggleLikeDTO.User && l.ForumPost.Id == toggleLikeDTO.PostId);

			if (like == null)
			{
				// If no like exists, create a new one if the DTO indicates a like
				if (toggleLikeDTO.Like)
				{
					var newLike = new LikeEntity
					{
						User = toggleLikeDTO.User,
						ForumPost = await _context.Posts.FindAsync(toggleLikeDTO.PostId),
						Like = true,
						ModifiedDate = DateTime.UtcNow
					};

					if (newLike.ForumPost == null)
					{
						throw new InvalidOperationException("Post not found.");
					}

					await _context.Likes.AddAsync(newLike);
				}
			}
			else
			{
				// If a like exists, update it based on the DTO
				like.Like = toggleLikeDTO.Like;
				like.ModifiedDate = DateTime.UtcNow;
			}

			await _context.SaveChangesAsync();
		}
	}
}