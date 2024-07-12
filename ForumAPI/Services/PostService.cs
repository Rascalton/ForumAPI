
using ForumAPI.Models.DTOs;

namespace ForumAPI.Services
{
    /// <summary>
    /// Represents a service for managing posts in a forum.
    /// </summary>
    public class PostService
    {
        /// <summary>
        /// Retrieves posts from the data store.
        /// </summary>
        /// <returns>A list of post DTOs.</returns>
        private async Task<List<PostDTO>> GetPostsFromTheDataStore()
        {
            // Create dummy data
            var posts = new List<PostDTO>
            {
                new PostDTO { Id = 1, Post = "Hello, World!", PostedDate = DateTime.Now },
                new PostDTO { Id = 2, Post = "Goodbye, World!", PostedDate = DateTime.Now }
            };

            return posts;
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