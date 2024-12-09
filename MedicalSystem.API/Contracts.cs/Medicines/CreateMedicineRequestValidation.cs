using FluentValidation;
using MedicalSystem.API.Contracts.cs.MedicalRecord;

namespace MedicalSystem.API.Contracts.cs.Medicines
{
	public class CreateMedicineRequestValidation : AbstractValidator<CreateMedicineRequest>
	{
		public CreateMedicineRequestValidation()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.NotNull();

			RuleFor(x => x.Price)
				.NotEmpty()
				.NotNull();

			RuleFor(x => x.Status)
				.NotEmpty()
				.NotNull();

			RuleFor(x => x.InStock)
				.NotEmpty()
				.NotNull();

			RuleFor(x => x.Measure)
				.NotEmpty()
				.NotNull();
		}
	}
}
