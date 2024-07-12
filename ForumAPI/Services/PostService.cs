using ForumAPI.Data;
using ForumAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ForumAPI.Services
{
    /// <summary>
    /// Represents a service for managing posts in a forum.
    /// </summary>
    public class PostService
    {

         // The data context used to interact with the data store.
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostService"/> class.
        /// </summary>
        /// <param name="context">The data context used to interact with the data store.</param>
        /// <exception cref="ArgumentNullException">Thrown when the context is null.</exception>
        public PostService(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves posts from the data store.
        /// </summary>
        /// <returns>A list of post DTOs.</returns>
        private async Task<List<PostDTO>> GetPostsFromTheDataStore()
        {
        var messages = await _context.Posts
            .Select(m => new PostDTO
            {
                Id = m.Id,
                Author = m.Author,
                Message = m.Message,
                PostedDate = m.PostedDate
            })
            .ToListAsync();

        return messages;
        }

        /// <summary>
        /// Retrieves all messages asynchronously.
        /// </summary>
        /// <returns>A list of post DTOs.</returns>
        public async Task<List<PostDTO>> GetAllMessagesAsync()
        {
            return await GetPostsFromTheDataStore();
        }
    }
}