namespace MedicalSystem.API.Contracts.cs.Authentication
{
	public record RegisterRequest(
		string Email,
		string FullName,
		string Password
	);
}
