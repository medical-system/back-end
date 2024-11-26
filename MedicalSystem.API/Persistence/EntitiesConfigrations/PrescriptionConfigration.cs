using MedicalSystem.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalSystem.API.Persistence.EntitiesConfigrations
{
	public class PrescriptionConfigration : IEntityTypeConfiguration<Prescription>
	{
		public void Configure(EntityTypeBuilder<Prescription> builder)
		{
			builder.Property(p => p.Id)
					.ValueGeneratedOnAdd()
					.UseIdentityColumn(1, 1);
		}
	}
}
