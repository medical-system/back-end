using FluentValidation;
using MedicalSystem.API.Contracts.cs.Prescription;

namespace MedicalSystem.API.Contracts.cs.Users
{
	public class UpdateUserRequestValidation : AbstractValidator<UpdateUserRequest>
	{
        public UpdateUserRequestValidation()
        {
			RuleFor(x => x.FullName)
			   .NotNull()
			   .NotEmpty();

			RuleFor(x => x.Email)
				 .NotEmpty()
				 .EmailAddress();

			RuleFor(x => x.Phone)
				 .NotEmpty()
				 .EmailAddress();
		}
	}
}
