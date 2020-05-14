using AspNetCore.Example.Application.Mapping.Dto.CompanyInformation;
using AspNetCore.Example.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AspNetCore.Example.Application.Mapping.Response.GetInfomationByDocument
{
    public class GetInfomationByDocumentResponse : Response
    {
        public GetInfomationByDocumentResponse()
        {
                
        }

        public GetInfomationByDocumentResponse(CompanyInformationDto companyInformationDto)
        {
            CompanyInformationDto = companyInformationDto;
        }

        public CompanyInformationDto CompanyInformationDto { get; set; }
    }
}
