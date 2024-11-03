namespace MedicalSystem.API.Contracts.cs.Authentication
{
	public record ResetPasswordRequest
	(
		string Email,
		string Password,
		string ConfirmPassword
	);
}
