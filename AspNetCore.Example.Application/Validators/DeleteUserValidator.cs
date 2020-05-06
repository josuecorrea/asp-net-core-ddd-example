using AspNetCore.Example.Application.Mapping.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Example.Application.Validators
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserValidator()
        {
            IdValidate();
        }

        private void IdValidate()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Código inválido. Por favor verificar os dados informados!");
        }

    }
}
