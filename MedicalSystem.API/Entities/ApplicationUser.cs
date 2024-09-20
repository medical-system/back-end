﻿using Microsoft.AspNetCore.Identity;

namespace MedicalSystem.API.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public bool IsDisabled { get; set; }
		public List<RefreshToken> RefreshToken { get; set; } = [];
	}
}
