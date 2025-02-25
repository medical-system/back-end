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
			builder.OwnsMany(medical => medical.Prescriptions, prescriptions => prescriptions.WithOwner());
			builder.Property(mr => mr.Id)
				.ValueGeneratedOnAdd()
				.UseIdentityColumn(1, 1);

			builder.HasOne(m => m.CreatedBy)
			.WithMany()  // No navigation property back
			.HasForeignKey(m => m.CreatedById)
			.OnDelete(DeleteBehavior.NoAction);

			// UpdatedBy relationship - Optional, restrict deletion (instead of SetNull)
			builder.HasOne(m => m.UpdatedBy)
				.WithMany()
				.HasForeignKey(m => m.UpdatedById)
				.OnDelete(DeleteBehavior.NoAction)  // Changed to Restrict
				.IsRequired(false);

			// Patient relationship - Optional, restrict deletion (instead of SetNull)
			builder.HasOne<ApplicationUser>()  // No navigation property in MedicalRecord
				.WithMany(u => u.MedicalRecords)
				.HasForeignKey(m => m.PatientId)
				.OnDelete(DeleteBehavior.NoAction)  // Changed to Restrict
				.IsRequired(false);
		}
	}
}
