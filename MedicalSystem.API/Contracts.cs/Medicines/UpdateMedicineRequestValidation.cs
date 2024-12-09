using FluentValidation;

namespace MedicalSystem.API.Contracts.cs.Medicines
{
	public class UpdateMedicineRequestValidation : AbstractValidator<UpdateMedicineRequest>
	{
        public UpdateMedicineRequestValidation()
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
