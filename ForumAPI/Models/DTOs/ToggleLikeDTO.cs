namespace ForumAPI.Models.DTOs;

public class ToggleLikeDTO
{
    public string User { get; set; } = string.Empty;
    public bool Like { get; set; }
    public int PostId { get; set; }
    // Add other necessary properties
}