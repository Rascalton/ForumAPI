public class CreateCommentDTO
{
    public string Content { get; set; } = string.Empty;
    public int ForumPostId { get; set; }
    public string Author { get; set; } = string.Empty;
}