using Microsoft.AspNetCore.Identity;

namespace MedicalSystem.API.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string FullName { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public string BloodyGroup { get; set; } = string.Empty;
		public int Age { get; set; }
		public bool IsDisabled { get; set; }
		public List<RefreshToken> RefreshToken { get; set; } = [];
	}
}
