using MedicalSystem.API.Abstractions.Consts;
using MedicalSystem.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalSystem.API.Persistence.EntitiesConfigrations
{
	public class RoleConfigration : IEntityTypeConfiguration<ApplicationRole>
	{
		public void Configure(EntityTypeBuilder<ApplicationRole> builder)
		{
			builder.HasData([
				new ApplicationRole
				{
					Id =DefaultRoless.AdminRoleId,
					Name = DefaultRoless.Admin,
					NormalizedName = DefaultRoless.Admin.ToUpper(),
					ConcurrencyStamp = DefaultRoless.AdminRoleConcurrencyStamp
				},
				new ApplicationRole
				{
					Id= DefaultRoless.UserRoleId,
					Name = DefaultRoless.User,
					NormalizedName = DefaultRoless.User.ToUpper(),
					ConcurrencyStamp = DefaultRoless.UserRoleConcurrencyStamp
				}
			]);
		}
	}
}
