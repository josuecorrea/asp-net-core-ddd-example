using AspNetCore.Example.Application.Mapping.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Example.Api.Validators
{
    public class UpdateUserValidator: AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {
            NameValidate();
            EmailValidate();
            GroupIdValidate();
        }

        private void NameValidate()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .Length(10, 120)
                .WithMessage("Nome inválido. Por favor verificar os dados informados!");
        }
        private void EmailValidate()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .Length(10, 120)
                .WithMessage("E-mail inválido. Por favor verificar os dados informados!");
        }
        private void GroupIdValidate()
        {
            RuleFor(c => c.Group)
                .IsInEnum()
                .NotEmpty()
                .NotNull()
                .WithMessage("Grupo inválido. Por favor verificar os dados informados!");
        }
    }
}
