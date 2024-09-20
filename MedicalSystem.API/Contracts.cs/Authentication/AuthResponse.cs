namespace MedicalSystem.API.Contracts.cs.Authentication
{
	public record AuthResponse
	(
		 string Id,
		 string? Email,
		 string FullName,
		 string Token,
		 int ExpiresIn,
		 string RefreshToken,
		 DateTime RefreshTokenExpiration
	);
}
