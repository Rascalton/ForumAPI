public class CommentDTO
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime PostedDate { get; set; }
    public string Author { get; set; }= string.Empty;
}