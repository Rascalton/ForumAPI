namespace ForumAPI.Models.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("Likes")]
public class LikeEntity
{
	[Key]
	public int Id { get; set; }
	[Required]
	public string User { get; set; } = string.Empty;
	[Required]
    public PostEntity ForumPost { get; set; }
    [Required]
    public bool Like { get; set; }
    [Required]
    public DateTime ModifiedDate { get; set; }

	// Additional fields specific to the database entity
}