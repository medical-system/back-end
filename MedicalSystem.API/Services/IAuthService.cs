using MedicalSystem.API.Abstractions;
using MedicalSystem.API.Contracts.cs.Authentication;

namespace MedicalSystem.API.Services
{
	public interface IAuthService
	{
		Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
		Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
		Task<Result<AuthResponse>> GetRefreshToken(string token, string refreshToken, CancellationToken cancellationToken);
		Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken);
	}
}
