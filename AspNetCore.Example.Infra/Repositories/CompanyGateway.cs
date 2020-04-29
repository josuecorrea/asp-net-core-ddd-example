using AutoMapper;
using AspNetCore.Example.Domain.Contracts.Repositories;
using AspNetCore.Example.Domain.Entities;
using AspNetCore.Example.Infra.Models.CompanyInformation;
using AspNetCore.Example.Infra.Repositories.Base;
using AspNetCore.Example.Infra.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace AspNetCore.Example.Infra.Repositories
{
    public class CompanyGateway : BaseHttp<HttpParam, CompanyInformationModel>, ICompanyGateway
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public CompanyGateway(IConfiguration configuration, IMapper mapper, ICompanyRepository companyRepository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public async Task<CompanyInformation> GetCompanyInformationByCnpj(string cnpj)
        {
            var company = await _companyRepository.GetCompanyInformationByCnpj(cnpj);

            if (company == null)
            {
                company = await GetCompanyInformationOnExternalService(cnpj);

                if (company != null)
                {
                    await _companyRepository.Insert(company);
                }
            }

            return company;
        }

        private async Task<CompanyInformation> GetCompanyInformationOnExternalService(string cnpj)
        {
            var apiUrl = $"";
            var cnpjServiceToken = _configuration["AppSettings:CnpjServiceToken"];

            var param = new HttpParam(apiUrl, cnpjServiceToken, true);

            var result = await GetWithAuthorization(param);

            var companyInformation = _mapper.Map<CompanyInformation>(result);

            return companyInformation;
        }      
    }
}
