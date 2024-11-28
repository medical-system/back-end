namespace MedicalSystem.API.Contracts.cs.Users
{
	public record UpdateUserRequest
	(
		string FullName,
		string Email,
		string Phone
	);
}
