using FluentValidation;
using MedicalSystem.API.Contracts.cs.Patients;

namespace MedicalSystem.API.Contracts.cs.Prescription
{
	public class CreatePrescriptionRequestValidation : AbstractValidator<CreatePrescriptionRequest>
	{
        public CreatePrescriptionRequestValidation()
        {
			RuleFor(x => x.ItemPrice)
				.NotEmpty()
				.NotNull();

			RuleFor(x => x.Dosage)
					.NotEmpty()
					.NotNull();
			
			RuleFor(x => x.Instraction)
				.NotEmpty()
				.NotNull();
			
			RuleFor(x => x.Quantity)
				.NotEmpty()
				.NotNull();

			RuleFor(x => x.Amout)
				.NotEmpty()
				.NotNull();

		}
    }
}
