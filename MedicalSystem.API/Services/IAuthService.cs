using MedicalSystem.API.Contracts.cs.Authentication;

namespace MedicalSystem.API.Services
{
	public interface IAuthService
	{
		Task<bool> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
	}
}
