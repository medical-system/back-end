namespace MedicalSystem.API.Contracts.cs.Authentication
{
	public record LoginRequest
	(
		string Email,
		string Password
	);
}
