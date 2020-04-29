using System.Threading.Tasks;
using AspNetCore.Example.Domain.Entities;

namespace AspNetCore.Example.Domain.Contracts.Repositories
{
    public interface ICompanyRepository
    {
        Task<CompanyInformation> GetCompanyInformationByCnpj(string cnpj);
        Task Insert(CompanyInformation companyInformation);
    }
}
