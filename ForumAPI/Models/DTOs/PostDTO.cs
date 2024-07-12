namespace ForumAPI.Models.DTOs
{
    /// <summary>
    /// Represents a data transfer object for a post.
    /// </summary>
    public class PostDTO
    {
        /// <summary>
        /// Gets or sets the ID of the post.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the content of the post.
        /// </summary>
        public string Post { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date when the post was posted.
        /// </summary>
        public DateTime PostedDate { get; set; }
    }
}