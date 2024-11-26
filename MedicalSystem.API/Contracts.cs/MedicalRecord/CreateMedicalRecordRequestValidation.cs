using FluentValidation;

namespace MedicalSystem.API.Contracts.cs.MedicalRecord
{
    public class CreateMedicalRecordRequestValidation : AbstractValidator<CreateMedicalRecordRequest>
    {
        public CreateMedicalRecordRequestValidation()
        {
            RuleFor(x => x.Complaint)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Diagnosis)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Treatment)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.VitalSigns)
                .NotEmpty()
                .NotNull();
        }
    }
}
