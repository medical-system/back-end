using Microsoft.AspNetCore.Identity;

namespace MedicalSystem.API.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string FullName { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string ImageUrl { get; set; } = string.Empty;
		public string Gender { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public string BloodyGroup { get; set; } = string.Empty;
		public int Age { get; set; }
		public bool IsDisabled { get; set; }
		public string? DoctorId { get; set; }
		public ApplicationUser? Doctor { get; set; }
		public List<RefreshToken> RefreshToken { get; set; } = [];
		public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = [];
		public List<RefreshToken> RefreshTokens { get; set; } = [];
	}
}
