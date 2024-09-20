using Mapster;
using MedicalSystem.API.Abstractions;
using MedicalSystem.API.Authentication;
using MedicalSystem.API.Contracts.cs.Authentication;
using MedicalSystem.API.Entities;
using MedicalSystem.API.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;

namespace MedicalSystem.API.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IJwtProvider _jwtProvider;
		private readonly int _refreshTokenExpiryDays = 14;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public AuthService(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_jwtProvider = jwtProvider;
			_signInManager = signInManager;
		}
		public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
		{
			var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
			if (user == null)
				return Result.Failure<AuthResponse>(UserErrors.UserNotFound);

			if (!user.IsDisabled)
				return Result.Failure<AuthResponse>(UserErrors.DisabledUser);

			var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
			if (result.Succeeded)
			{
				var userRoles = await GetUserRolesAndPermissions(user, cancellationToken);

				var (token, expiresIn) = _jwtProvider.GenerateJwtToken(user, userRoles);

				var refreshToken = GenerateRefreshToken();

				var refreshTokenExpireOn = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

				user.RefreshToken.Add(new RefreshToken
				{
					Token = refreshToken,
					ExpiresOn = refreshTokenExpireOn
				});
				await _userManager.UpdateAsync(user);
				var response = new AuthResponse(user.Id, user.Email, user.UserName!, token, expiresIn, refreshToken, refreshTokenExpireOn);
				return Result.Success(response);
			}
			return Result.Failure<AuthResponse>(result.IsNotAllowed ? UserErrors.EmailNotConfirmed : UserErrors.InvalidCredentails);

		}
		public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
		{
			var emailExsists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
			if (emailExsists)
				return Result.Failure(UserErrors.DuplicatedEmail);
			var user = request.Adapt<ApplicationUser>();
			user.UserName = request.FullName;
			user.Email = request.Email;

			var result = await _userManager.CreateAsync(user, request.Password);

			if (result.Succeeded)
				return Result.Success();

			var error = result.Errors.First();
			return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
		}

		public async Task<Result<AuthResponse>> GetRefreshToken(string token, string refreshToken, CancellationToken cancellationToken)
		{
			var userId = _jwtProvider.ValidateJwtToken(token);
			if (userId is null)
				return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

			var user = await _userManager.FindByIdAsync(userId);
			if (user is null)
				return Result.Failure<AuthResponse>(UserErrors.UserNotFound);

			if (user.IsDisabled)
				return Result.Failure<AuthResponse>(UserErrors.DisabledUser);

			var userRefreshToken = user.RefreshToken.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);
			if (userRefreshToken is null)
				return Result.Failure<AuthResponse>(UserErrors.InvalidRefreshToken);

			userRefreshToken.RevokedOn = DateTime.UtcNow;

			var userRoles = await GetUserRolesAndPermissions(user, cancellationToken);

			var (newToken, newExpiresIn) = _jwtProvider.GenerateJwtToken(user, userRoles);

			var newrRfreshToken = GenerateRefreshToken();

			var refreshTokenExpireOn = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

			user.RefreshToken.Add(new RefreshToken
			{
				Token = refreshToken,
				ExpiresOn = refreshTokenExpireOn
			});
			await _userManager.UpdateAsync(user);
			var response = new AuthResponse(user.Id, user.Email, user.UserName!, newToken, newExpiresIn, newrRfreshToken, refreshTokenExpireOn);
			return Result.Success(response);
		}

		public async Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken)
		{
			var userId = _jwtProvider.ValidateJwtToken(token);
			if (userId is null)
				return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

			var user = await _userManager.FindByIdAsync(userId);
			if (user is null)
				return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

			var userRefreshToken = user.RefreshToken.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);
			if (userRefreshToken is null)
				return Result.Failure<AuthResponse>(UserErrors.InvalidRefreshToken);

			userRefreshToken.RevokedOn = DateTime.UtcNow;
			await _userManager.UpdateAsync(user);
			return Result.Success();
		}

		private async Task<IEnumerable<string>> GetUserRolesAndPermissions(ApplicationUser user, CancellationToken cancellationToken)
		{
			var userRoles = await _userManager.GetRolesAsync(user);
			return userRoles;
		}
		private static string GenerateRefreshToken()
		{
			return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
		}
	}
}
