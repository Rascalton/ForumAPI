namespace ForumAPI.Models.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("Posts")]
public class PostEntity
{
	[Key]
	public int Id { get; set; }
	[Required]
	public string Author { get; set; } = string.Empty;
	[Required]
	public string Message { get; set; } = string.Empty;
	public DateTime PostedDate { get; set; }
	public ICollection<CommentEntity> Comments { get; set; }

	// Additional fields specific to the database entity
}