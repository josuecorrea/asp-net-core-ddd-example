using AspNetCore.Example.Application.Mapping.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Example.Api.Validators
{
    public class ChangeLinkWithTheCompanyValidator: AbstractValidator<ChangeLinkWithTheCompanyRequest>
    {
        public ChangeLinkWithTheCompanyValidator()
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
