using AspNetCore.Example.Application.Mapping.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Example.Application.Validators
{
    public class RedefinePasswordValidadtor : AbstractValidator<RedefinePasswordRequest>
    {
        public RedefinePasswordValidadtor()
        {
            IdValidate();
            PasswordValidate();
        }
        private void PasswordValidate()
        {
            RuleFor(c => c.NewPassword)
                .NotEmpty()
                .NotNull()
                .Length(6, 12)
                .WithMessage("nova senha inválida. Por favor verificar os dados informados!");
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
