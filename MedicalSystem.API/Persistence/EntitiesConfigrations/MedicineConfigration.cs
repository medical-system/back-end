﻿using MedicalSystem.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalSystem.API.Persistence.EntitiesConfigrations
{
	public class MedicineConfigration : IEntityTypeConfiguration<Medicine>
	{
		public void Configure(EntityTypeBuilder<Medicine> builder)
		{
			builder.Property(m=>m.Price)
				.HasColumnType("decimal(18,2)");
		}
	}
}