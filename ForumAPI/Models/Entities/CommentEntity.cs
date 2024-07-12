using System.ComponentModel.DataAnnotations.Schema;

namespace ForumAPI.Models.Entities;

[Table("Comments")]
public class CommentEntity
{
	public int Id { get; set; }
	public string Content { get; set; } = string.Empty;
	public DateTime PostedDate { get; set; }
	public string Author { get; set; }  = string.Empty;
	public int ForumMessageId { get; set; }
	public PostEntity ForumPost { get; set; }
}