using MedicalSystem.API.Entities;

namespace MedicalSystem.API.Authentication
{
	public interface IJwtProvider
	{
		(string token, int expiresIn) GenerateJwtToken(ApplicationUser user, IEnumerable<string> roles);

		string? ValidateJwtToken(string token);
	}
}
