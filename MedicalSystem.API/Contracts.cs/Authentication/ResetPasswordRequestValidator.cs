﻿using FluentValidation;
using MedicalSystem.API.Abstractions.Consts;

namespace MedicalSystem.API.Contracts.cs.Authentication
{
	public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
	{
        public ResetPasswordRequestValidator()
        {
            RuleFor(x=>x.Email)
                .NotEmpty()
                .EmailAddress();

			RuleFor(x => x.Code)
				 .NotEmpty();

			RuleFor(x => x.NewPassword)
				.NotEmpty()
				.Matches(RegexPatterns.Password)
				.WithMessage("Passwrod should be at least 6 digits and should contains lower case, nonalphanumeric and uppercase");
		}
    }
}