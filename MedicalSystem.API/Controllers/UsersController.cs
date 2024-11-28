using MedicalSystem.API.Abstractions;
using MedicalSystem.API.Contracts.cs.Users;
using MedicalSystem.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;
		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPut("{id}/Update")]
		public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
		{
			var result = await _userService.UpdateAsync(id, request, cancellationToken);
			return result.IsSuccess ? NoContent() : result.ToProblem();
		}

		[HttpPut("{id}/toggle-status")]
		public async Task<IActionResult> ToggleStatus([FromRoute] string id)
		{
			var result = await _userService.ToggleStatus(id);
			return result.IsSuccess ? NoContent() : result.ToProblem();
		}

	}
}
