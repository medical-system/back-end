﻿using FluentValidation;

namespace MedicalSystem.API.Contracts.cs.Authentication
{
	public class ResendConfirmationEmailRequestValidator :AbstractValidator<ResendConfirmationEmailRequest>
	{
        public ResendConfirmationEmailRequestValidator()
        {
            RuleFor(x => x.Email)
				.NotEmpty()
				.EmailAddress();
		}
    }
}