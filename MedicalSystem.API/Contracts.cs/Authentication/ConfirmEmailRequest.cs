namespace MedicalSystem.API.Contracts.cs.Authentication
{
	public record ConfirmEmailRequest
	(
		string UserId,
		string Code
	);
}
