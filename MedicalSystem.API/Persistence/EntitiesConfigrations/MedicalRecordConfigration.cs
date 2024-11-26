using MedicalSystem.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace MedicalSystem.API.Persistence.EntitiesConfigrations
{
	public class MedicalRecordConfigration : IEntityTypeConfiguration<MedicalRecord>
	{
		public void Configure(EntityTypeBuilder<MedicalRecord> builder)
		{
			builder.Property(mr => mr.Id)
				.ValueGeneratedOnAdd()
				.UseIdentityColumn(1, 1);
		}
	}
}
