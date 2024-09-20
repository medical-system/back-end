using FluentValidation;
using MedicalSystem.API.Abstractions.Consts;

namespace MedicalSystem.API.Contracts.cs.Authentication
{
	public class LoginRequestValidator : AbstractValidator<LoginRequest>
	{
		public LoginRequestValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.EmailAddress();

			RuleFor(x => x.Password)
				.NotEmpty()
				.Matches(RegexPatterns.Password)
				.WithMessage("Passwrod should be at least 6 digits and should contains lower case, nonalphanumeric and uppercase");
		}
	}
}
