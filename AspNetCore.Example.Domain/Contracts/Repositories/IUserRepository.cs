using AspNetCore.Example.Domain.Entities.UserAgg;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCore.Example.Domain.Contracts.Repositories
{
    public interface IUserRepository : IRepositorio<User>
    {
        Task<bool> ExistsUser(string userName);
        Task<User> Login(string email);
        Task<List<User>> GetUsersByMasterId(Guid userMasterId);
        Task<bool> DeleteUserById(Guid userId);
        Task<bool> UpdateUser(User user);
        Task<bool> SetNewPassword(Guid id, string passwordNew);
        Task<User> GetUserById(Guid Id);
        Task<List<UserCompany>> GetAllUserCompany(Guid id);
        Task<bool> ChangeLinkWithTheCompany(Guid id,List<UserCompany> userCompanies);        
    }
}
