using AspNetCore.Example.Domain.Entities;
using System.Threading.Tasks;

namespace AspNetCore.Example.Infra.Repositories.Interfaces
{
    public interface ICompanyGateway
    {
        Task<CompanyInformation> GetCompanyInformationByCnpj(string cnpj);
    }
}
