using MedicalSystem.API.Contracts.cs.Authentication;
using MedicalSystem.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequest request,CancellationToken cancellationToken)
		{
			var result = await _authService.RegisterAsync(request);
			if (result)
				return Ok();

			return BadRequest();
		}
	}
}
