﻿using MedicalSystem.API.Abstractions.Consts;
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
					Id =DefaultRoles.AdminRoleId,
					Name = DefaultRoles.Admin,
					NormalizedName = DefaultRoles.Admin.ToUpper(),
					ConcurrencyStamp = DefaultRoles.AdminRoleConcurrencyStamp
				},
				new ApplicationRole
				{
					Id= DefaultRoles.UserRoleId,
					Name = DefaultRoles.User,
					NormalizedName = DefaultRoles.User.ToUpper(),
					ConcurrencyStamp = DefaultRoles.UserRoleConcurrencyStamp,
					IsDefault= true
				},
				new ApplicationRole
				{
					Id= DefaultRoles.PatientRoleId,
					Name = DefaultRoles.Patient,
					NormalizedName = DefaultRoles.Patient.ToUpper(),
					ConcurrencyStamp = DefaultRoles.PatientRoleConcurrencyStamp
				},
				new ApplicationRole
				{
					Id= DefaultRoles.DoctorRoleId,
					Name = DefaultRoles.Doctor,
					NormalizedName = DefaultRoles.Doctor.ToUpper(),
					ConcurrencyStamp = DefaultRoles.DoctorRoleConcurrencyStamp
				},
				new ApplicationRole
				{
					Id= DefaultRoles.ReceptionRoleId,
					Name = DefaultRoles.Reception,
					NormalizedName = DefaultRoles.Reception.ToUpper(),
					ConcurrencyStamp = DefaultRoles.ReceptionRoleConcurrencyStamp
				}
			]);
		}
	}
}
