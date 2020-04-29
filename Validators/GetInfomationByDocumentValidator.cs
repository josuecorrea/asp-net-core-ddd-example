using AspNetCore.Example.Application.Mapping.Request;
using FluentValidation;

namespace AspNetCore.Example.Api.Validators
{
    public class GetInfomationByDocumentValidator : AbstractValidator<GetInfomationByDocumentRequest>
    {
        public GetInfomationByDocumentValidator()
        {
            DocumentValidate();
        }

        void DocumentValidate()
        {
            RuleFor(c => c.Document)
               .NotEmpty()
               .NotNull()
               .Length(11, 14)
               .WithMessage("Documento inválido. Por favor verificar os dados informados!");
        }
    }
}
