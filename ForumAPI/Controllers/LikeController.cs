using Microsoft.AspNetCore.Mvc;
using ForumAPI.Services;
using ForumAPI.Models.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ForumAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LikeController : ControllerBase
	{
		private readonly LikeService _likeService;

		public LikeController(LikeService likeService)
		{
			_likeService = likeService;
		}

		[HttpPost("toggle"),Authorize]
		public async Task<IActionResult> ToggleLike([FromBody] ToggleLikeDTO toggleLikeDTO)
		{
			try
			{
				await _likeService.ToggleLikeAsync(toggleLikeDTO);
				return Ok(new { message = "Like status updated successfully." });
			}
			catch (System.Exception ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}
	}
}