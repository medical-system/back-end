using FluentValidation;

namespace MedicalSystem.API.Contracts.cs.Authentication
{
	public class ForgetPasswrodRequestValidator : AbstractValidator<ForgetPasswrodRequest>
	{
		public ForgetPasswrodRequestValidator()
		{
			RuleFor(x => x.Email)
			   .NotEmpty()
			   .EmailAddress();
		}
	}
}
