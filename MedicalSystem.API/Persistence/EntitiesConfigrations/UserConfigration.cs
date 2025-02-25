using MedicalSystem.API.Abstractions.Consts;
using MedicalSystem.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace MedicalSystem.API.Persistence.EntitiesConfigrations
{
	public class UserConfigration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.OwnsMany(x => x.RefreshTokens).ToTable("RefreshTokens").WithOwner().HasForeignKey("UserId");
			builder.HasOne(u => u.Doctor)
			   .WithMany()
			   .HasForeignKey(u => u.DoctorId)
			   .OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(u => u.MedicalRecords)
			.WithOne()
			.HasForeignKey(mr => mr.PatientId)
			.OnDelete(DeleteBehavior.NoAction);

			builder.Property(x => x.FirstName).HasMaxLength(100);
			builder.Property(x => x.LastName).HasMaxLength(100);

			var passwordHasher = new PasswordHasher<ApplicationUser>();
			builder.HasData(
				new ApplicationUser
				{
					Id = DefaultUsers.AdminId,
					FirstName = "Medical System",
					LastName = "Admin",
					UserName = DefaultUsers.AdminEmail,
					FullName = DefaultUsers.AdminFullName,
					NormalizedUserName = DefaultUsers.AdminEmail.ToUpper(),
					Email = DefaultUsers.AdminEmail,
					NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
					SecurityStamp = DefaultUsers.AdminSecurityStamp,
					ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
					EmailConfirmed = true,
					PasswordHash = passwordHasher.HashPassword(null!, DefaultUsers.AdminPassword),
					Password = DefaultUsers.AdminPassword
				});
		}
	}
}
