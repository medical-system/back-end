using MedicalSystem.API.Abstractions;
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
		private readonly ILogger<AuthController> _logger;
		public AuthController(IAuthService authService, ILogger<AuthController> logger)
		{
			_authService = authService;
			_logger = logger;
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Logging with email: {email} and password:{passwrod}", request.Email, request.Password);
			var authResult = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);
			return authResult.IsSuccess ? Ok(authResult.Value) : authResult.ToProblem();
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
		{
			var result = await _authService.RegisterAsync(request);
			return result.IsSuccess ? Ok() : result.ToProblem();
		}

		[HttpPost("refresh-token")]
		public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
		{
			var result = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
			return result.IsSuccess ? Ok() : result.ToProblem();
		}

		[HttpPost("revoke-refresh-token")]
		public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
		{
			var result = await _authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
			return result.IsSuccess ? Ok() : result.ToProblem();
		}

		[HttpPost("confirm-email")]
		public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
		{
			var result = await _authService.ConfirmEmailAsync(request);
			return result.IsSuccess ? Ok() : result.ToProblem();
		}

		[HttpPost("resend-confirm-email")]
		public async Task<IActionResult> ResendConfirmEmail([FromBody] ResendConfirmationEmailRequest request)
		{
			var result = await _authService.ResendConfirmEmailAsync(request);
			return result.IsSuccess ? Ok() : result.ToProblem();
		}

		[HttpPost("forget-password")]
		public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswrodRequest request)
		{
			var result = await _authService.SendResetPasswordCodeAsync(request.Email);
			return result.IsSuccess ? Ok() : result.ToProblem();
		}

		[HttpPost("reset-password")]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
		{
			var result = await _authService.ResetPasswordAsync(request);
			return result.IsSuccess ? Ok() : result.ToProblem();
		}

	}
}
