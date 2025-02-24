using FluentValidation;
using MedicalSystem.API.Abstractions.Consts;
using MedicalSystem.API.Contracts.cs.Authentication;
using MedicalSystem.API.Contracts.cs.Files;

namespace MedicalSystem.API.Contracts.cs.Patients
{
	public class CreatePatientRequestValidation : AbstractValidator<CreatePatientRequest>
	{
		public CreatePatientRequestValidation()
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.EmailAddress();

			RuleFor(x => x.FullName)
				.NotEmpty()
				.Length(3, 100);

			RuleFor(x=>x.Age)
				.NotEmpty()
				.NotNull()
				.GreaterThan(0)
				.LessThan(150);

			RuleFor(x => x.ImageUrl)
				.SetValidator(new BlockedSignaturesValidator()!)
				.SetValidator(new FileSizeValidator()!);

			RuleFor(x => x.BloodyGroup)
				.NotEmpty()
				.NotNull();
				//.Matches(RegexPatterns.BloodyGroup);

			RuleFor(x => x.Password)
				.NotEmpty()
				.Matches(RegexPatterns.Password)
				.WithMessage("Passwrod should be at least 6 digits and should contains lower case, nonalphanumeric and uppercase");
		}
	}
}
