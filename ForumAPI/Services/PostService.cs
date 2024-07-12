using ForumAPI.Data;
using ForumAPI.Models.DTOs;
using ForumAPI.Models.Entities;
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
        private async Task<List<PostDTO>> GetPostsFromTheDataStore(string? author = null, DateTime? startDate = null, DateTime? endDate = null, int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Posts.AsQueryable();
        
            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(m => m.Author == author);
            }
        
            if (startDate.HasValue)
            {
                query = query.Where(m => m.PostedDate >= startDate.Value);
            }
        
            if (endDate.HasValue)
            {
                query = query.Where(m => m.PostedDate <= endDate.Value);
            }
        
            // Apply pagination
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        
            var messages = await query
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
        /// Retrieves all messages asynchronously with pagination.
        /// </summary>
        /// <param name="author">The author to filter by.</param>
        /// <param name="startDate">The start date to filter by.</param>
        /// <param name="endDate">The end date to filter by.</param>
        /// <param name="pageNumber">The page number for pagination.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A list of post DTOs.</returns>
        public async Task<List<PostDTO>> GetAllMessagesAsync(string? author = null, DateTime? startDate = null, DateTime? endDate = null, int pageNumber = 1, int pageSize = 10)
        {
            return await GetPostsFromTheDataStore(author, startDate, endDate, pageNumber, pageSize);
        }

        /// <summary>
        /// Retrieves a message by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the message.</param>
        /// <returns>The post DTO.</returns>
        public async Task<PostDTO?> GetPostByIdAsync(int id)
        {
            var message = await _context.Posts
                .Where(m => m.Id == id)
                .Select(m => new PostDTO
                {
                    Id = m.Id,
                    Author = m.Author,
                    Message = m.Message,
                    PostedDate = m.PostedDate
                })
                .FirstOrDefaultAsync();

            return message;
        }

        /// <summary>
        /// Adds a message asynchronously.
        /// </summary>
        /// <param name="createPostDTO">The post DTO to add.</param>
        /// <returns>The added post DTO.</returns>
        public async Task<PostDTO> AddMessageAsync(CreatePostDTO createPostDTO)
        {
            var messageEntity = new PostEntity
            {
                Author = createPostDTO.Author,
                Message = createPostDTO.Message,
                PostedDate = DateTime.UtcNow 
                                             
            };

            _context.Posts.Add(messageEntity);
            await _context.SaveChangesAsync();

            return new PostDTO
            {
                Id = messageEntity.Id,
                Author = messageEntity.Author,
                Message = messageEntity.Message,
                PostedDate = messageEntity.PostedDate
            };
        }


    }
}