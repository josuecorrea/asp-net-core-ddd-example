using AspNetCore.Example.Domain.Contracts.Repositories;
using AspNetCore.Example.Domain.Entities;
using AspNetCore.Example.Infra.Repositories.Base;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace AspNetCore.Example.Infra.Repositories
{
    public class CompanyRepository : BaseRepository<CompanyInformation>, ICompanyRepository
    {
        private static MongoDBContext<CompanyInformation> _dbContext = new MongoDBContext<CompanyInformation>();

        public CompanyRepository(): base(_dbContext) { }

        public async Task<CompanyInformation> GetCompanyInformationByCnpj(string cnpj)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.Cnpj.Equals(cnpj)).ConfigureAwait(false);

            return await result.FirstOrDefaultAsync();
        }  
        
        public async Task Insert(CompanyInformation companyInformation)
        {
            if (!companyInformation.Id.HasValue)
            {
                companyInformation.Id = Guid.NewGuid();
            }

            await _dbContext.GetColection.InsertOneAsync(companyInformation).ConfigureAwait(false);
        }
    }
}
