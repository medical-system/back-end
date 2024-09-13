using Mapster;
using MedicalSystem.API.Authentication;
using MedicalSystem.API.Contracts.cs.Authentication;
using MedicalSystem.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.API.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
		{
			var emailExsists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
			if(emailExsists)
				return false;
			var user = request.Adapt<ApplicationUser>();
			user.UserName=request.FullName;
			user.Email=request.Email;
			
			var result = await _userManager.CreateAsync(user, request.Password);

			if (!result.Succeeded)
				return false;

			return true;
		}
	}
}
