using AspNetCore.Example.Domain.Commons;
using AspNetCore.Example.Domain.Entities;
using System.Collections.Generic;

namespace AspNetCore.Example.Application.Mapping.Result.GetInfomationByDocument
{
    public class GetInfomationByDocumentResponse : BaseResult
    {
        public GetInfomationByDocumentResponse(string error)
        {
            AddMessageError(error);
        }

        public GetInfomationByDocumentResponse(List<string> errors)
        {
            Erros = errors;            
        }

        public GetInfomationByDocumentResponse(CompanyInformationDto companyInformation)           
        {
            CompanyInformation = companyInformation;
        }

        public CompanyInformationDto CompanyInformation { get; private set; }
    }
}
