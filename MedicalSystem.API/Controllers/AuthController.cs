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
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
		{
			var result = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);
			return result.IsSuccess ? Ok() : BadRequest(result.Error);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequest request,CancellationToken cancellationToken)
		{
			var result = await _authService.RegisterAsync(request);
			return result.IsSuccess ? Ok() : BadRequest(result.Error);
		}

		[HttpPost("refresh-token")]
		public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
		{
			var result = await _authService.GetRefreshToken(request.Token, request.RefreshToken, cancellationToken);
			return result.IsSuccess ? Ok() : BadRequest(result.Error);
		}

		[HttpPost("revoke-refresh-token")]
		public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
		{
			var result = await _authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
			return result.IsSuccess ? Ok() : BadRequest(result.Error);
		}
	}
}
