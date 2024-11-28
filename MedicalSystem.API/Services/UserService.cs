using MedicalSystem.API.Abstractions;
using MedicalSystem.API.Contracts.cs.Patients;
using MedicalSystem.API.Contracts.cs.Users;
using MedicalSystem.API.Entities;
using MedicalSystem.API.Errors;
using MedicalSystem.API.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.API.Services
{
	public class UserService : IUserService
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ILogger<UserService> _logger;
		public UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<UserService> logger)
		{
			_context = context;
			_userManager = userManager;
			_logger = logger;
		}
		public async Task<Result> ToggleStatus(string id)
		{
			if (await _userManager.FindByIdAsync(id) is not { } user)
				return Result.Failure<PatientsResponse>(UserErrors.UserNotFound);

			user.IsDisabled = !user.IsDisabled;
			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
				return Result.Success();

			var error = result.Errors.First();
			return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
		}
		public async Task<Result> UpdateAsync(string id, UpdateUserRequest request, CancellationToken cancellationToken = default)
		{
			var emailIsExist = await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id, cancellationToken);
			if (emailIsExist)
				return Result.Failure(UserErrors.DuplicatedEmail);

			var user = await _userManager.FindByIdAsync(id);
			if (user is null)
				return Result.Failure(UserErrors.UserNotFound);

			string fullName = request.FullName;
			string[] nameParts = fullName.Split(' ');
			user.FirstName = nameParts[0];
			user.LastName = nameParts.Length > 1 ? nameParts[^1] : string.Empty;
			user.FullName = request.FullName;
			user.UserName = request.Email;
			user.PhoneNumber = request.Phone;
			user.NormalizedUserName = request.Email.ToUpper();
			user.Email = request.Email;
			user.EmailConfirmed = true;

			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
				return Result.Success();

			var error = result.Errors.First();
			return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
		}
	}
}
