using Hangfire;
using Mapster;
using MedicalSystem.API.Abstractions;
using MedicalSystem.API.Abstractions.Consts;
using MedicalSystem.API.Authentication;
using MedicalSystem.API.Contracts.cs.Authentication;
using MedicalSystem.API.Entities;
using MedicalSystem.API.Errors;
using MedicalSystem.API.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;

namespace MedicalSystem.API.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManger;
		private readonly IJwtProvider _jwtProvider;
		private readonly int _refreshTokenExpiryDays = 14;
		private readonly ILogger<AuthService> _logger;
		private readonly IEmailSender _emailSender;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public AuthService(UserManager<ApplicationUser> userManager,
			IJwtProvider jwtProvider,
			SignInManager<ApplicationUser> signInManager,
			IEmailSender emailSender,
			ILogger<AuthService> logger,
			IHttpContextAccessor httpContextAccessor)
		{
			_userManger = userManager;
			_jwtProvider = jwtProvider;
			_signInManager = signInManager;
			_httpContextAccessor = httpContextAccessor;
			_emailSender = emailSender;
			_logger = logger;
		}
		public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
		{
			var user = await _userManger.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
			if (user == null)
				return Result.Failure<AuthResponse>(UserErrors.UserNotFound);

			if (user.IsDisabled)
				return Result.Failure<AuthResponse>(UserErrors.DisabledUser);

			var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
			if (result.Succeeded)
			{
				var userRoles = await GetUserRoles(user, cancellationToken);

				var (token, expiresIn) = _jwtProvider.GenerateJwtToken(user, userRoles);

				var refreshToken = GenerateRefreshToken();

				var refreshTokenExpireOn = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

				user.RefreshToken.Add(new RefreshToken
				{
					Token = refreshToken,
					ExpiresOn = refreshTokenExpireOn
				});
				await _userManger.UpdateAsync(user);
				var response = new AuthResponse(user.Id, user.Email, user.UserName!, token, expiresIn, refreshToken, refreshTokenExpireOn);
				return Result.Success(response);
			}
			return Result.Failure<AuthResponse>(result.IsNotAllowed ? UserErrors.EmailNotConfirmed : UserErrors.InvalidCredentails);

		}
		public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
		{
			var emailExsists = await _userManger.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
			if (emailExsists)
				return Result.Failure(UserErrors.DuplicatedEmail);
			var user = request.Adapt<ApplicationUser>();
			user.UserName = request.Email;
			user.FullName =	request.FullName;
			user.Email = request.Email;
			user.EmailConfirmed = true;
			user.Password = request.Password;

			var result = await _userManger.CreateAsync(user, request.Password);

			if (result.Succeeded)
			{
				//var code = await _userManger.GenerateEmailConfirmationTokenAsync(user);
				//code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
				//_logger.LogInformation("Confirmation Code {code}", code);

				//await SendConfirmationEmailAsync(user, code);
				await _userManger.AddToRoleAsync(user, DefaultRoles.User);

				return Result.Success();
			}

			var error = result.Errors.First();
			return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
		}

		public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken)
		{
			var userId = _jwtProvider.ValidateJwtToken(token);
			if (userId is null)
				return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

			var user = await _userManger.FindByIdAsync(userId);
			if (user is null)
				return Result.Failure<AuthResponse>(UserErrors.UserNotFound);

			if (user.IsDisabled)
				return Result.Failure<AuthResponse>(UserErrors.DisabledUser);

			var userRefreshToken = user.RefreshToken.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);
			if (userRefreshToken is null)
				return Result.Failure<AuthResponse>(UserErrors.InvalidRefreshToken);

			userRefreshToken.RevokedOn = DateTime.UtcNow;

			var userRoles = await GetUserRoles(user, cancellationToken);

			var (newToken, newExpiresIn) = _jwtProvider.GenerateJwtToken(user, userRoles);

			var newrRfreshToken = GenerateRefreshToken();

			var refreshTokenExpireOn = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

			user.RefreshToken.Add(new RefreshToken
			{
				Token = refreshToken,
				ExpiresOn = refreshTokenExpireOn
			});
			await _userManger.UpdateAsync(user);
			var response = new AuthResponse(user.Id, user.Email, user.UserName!, newToken, newExpiresIn, newrRfreshToken, refreshTokenExpireOn);
			return Result.Success(response);
		}

		public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request)
		{
			var user = await _userManger.FindByIdAsync(request.UserId);
			if (user is null)
				return Result.Failure(UserErrors.UserNotFound);

			if (user.EmailConfirmed)
				return Result.Failure(UserErrors.DuplicatedConfirmation);

			var code = request.Code;
			try
			{
				code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
			}
			catch (FormatException)
			{
				return Result.Failure(UserErrors.InvalidCode);
			}
			var result = await _userManger.ConfirmEmailAsync(user, code);
			if (result.Succeeded)
			{
				await _userManger.AddToRoleAsync(user, DefaultRoles.User);
				return Result.Success();
			}
			var error = result.Errors.First();
			return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
		}

		public async Task<Result> ResendConfirmEmailAsync(ResendConfirmationEmailRequest request)
		{
			var user = await _userManger.FindByEmailAsync(request.Email);
			if (user is null)
				return Result.Success();

			if (user.EmailConfirmed)
				return Result.Failure(UserErrors.DuplicatedConfirmation);

			var code = await _userManger.GenerateEmailConfirmationTokenAsync(user);
			code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
			_logger.LogInformation("Confirmation Code {code}", code);

			await SendConfirmationEmailAsync(user, code);

			return Result.Success();
		}

		public async Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken)
		{
			var userId = _jwtProvider.ValidateJwtToken(token);
			if (userId is null)
				return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

			var user = await _userManger.FindByIdAsync(userId);
			if (user is null)
				return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

			var userRefreshToken = user.RefreshToken.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);
			if (userRefreshToken is null)
				return Result.Failure<AuthResponse>(UserErrors.InvalidRefreshToken);

			userRefreshToken.RevokedOn = DateTime.UtcNow;
			await _userManger.UpdateAsync(user);
			return Result.Success();
		}

		public async Task<Result> SendResetPasswordCodeAsync(string email)
		{

			var user = await _userManger.FindByEmailAsync(email);
			if (user is null)
				return Result.Success();

			if (!user.EmailConfirmed)
				return Result.Failure(UserErrors.EmailNotConfirmed);

			var code = await _userManger.GeneratePasswordResetTokenAsync(user);
			code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
			_logger.LogInformation("Reset Password Code {code}", code);

			await SendResetPasswordEmailAsync(user, code);

			return Result.Success();
		}

		public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
		{
			var user = await _userManger.FindByEmailAsync(request.Email);
			if (user is null || user.EmailConfirmed)
				return Result.Failure(UserErrors.InvalidCode);

			IdentityResult result;
			try
			{
				var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
				result = await _userManger.ResetPasswordAsync(user, code, request.NewPassword);
			}
			catch (FormatException)
			{
				result = IdentityResult.Failed(_userManger.ErrorDescriber.InvalidToken());
			}

			if (result.Succeeded)
				return Result.Success();

			var error = result.Errors.First();
			return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status401Unauthorized));
		}

		private async Task<IEnumerable<string>> GetUserRoles(ApplicationUser user, CancellationToken cancellationToken)
		{
			var userRoles = await _userManger.GetRolesAsync(user);
			return userRoles;
		}
		private static string GenerateRefreshToken()
		{
			return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
		}
		private async Task SendConfirmationEmailAsync(ApplicationUser user, string code)
		{
			var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

			var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
				new Dictionary<string, string>
				{
					{"{{name}}",user.FirstName },
					{ "{{action_url}}",$"{origin}/auth/emailConfirmation?userId={user.Id}&code={code}" }
				}
			);
			BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "✅ Survay Basket: Confirm your email", emailBody));

			await Task.CompletedTask;
		}
		private async Task SendResetPasswordEmailAsync(ApplicationUser user, string code)
		{
			var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

			var emailBody = EmailBodyBuilder.GenerateEmailBody("ForgetPassword",
				new Dictionary<string, string>
				{
					{"{{name}}",user.FirstName },
					{ "{{action_url}}",$"{origin}/auth/forgetPassword?email={user.Email}&code={code}" }
				}
			);
			BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "✅ Survay Basket: Reset your password", emailBody));

			await Task.CompletedTask;
		}
	}
}
