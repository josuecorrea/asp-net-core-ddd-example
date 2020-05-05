using AspNetCore.Example.Domain.Contracts.Repositories;
using AspNetCore.Example.Domain.Entities.UserAgg;
using AspNetCore.Example.Infra.Repositories.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Example.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private static MongoDBContext<User> _dbContext = new MongoDBContext<User>();

        public UserRepository() : base(_dbContext) { }

        public override void Add(User entity)
        {
            if (!entity.Id.HasValue)
                entity.Id = Guid.NewGuid();

            base.Add(entity);
        }

        public async Task<bool> ChangeLinkWithTheCompany(Guid id, List<UserCompany> userCompanies)
        {
            var user = await GetUserById(id);
            if (user == null)
                return false;
            var update = Builders<User>.Update.Set(c => c.Companies, userCompanies);
            await _dbContext.GetColection.UpdateOneAsync(c => c.Id == user.Id, update);
            return true;
        }

        public async Task DeleteUserById(Guid userId)
        {
            await _dbContext.GetColection.DeleteOneAsync(c => c.Id == userId);
        }

        public async Task<bool> ExistsUser(string userName)
        {
            var builder = Builders<User>.Filter;
            var filter = builder.Eq(user => user.Name, userName);

            var result = await _dbContext.GetColection.FindAsync(filter);

            if (result.Any())
                return true;

            return false;
        }

        public async Task<List<UserCompany>> GetAllUserCompany(Guid id)
        {
            var companies = new List<UserCompany>();

            var queryResult = await _dbContext
                                .GetColection
                                .FindAsync(c => c.Id == id);

            companies = queryResult?.FirstOrDefault().Companies;

            return companies;
        }

        public async Task<User> GetUserById(Guid Id)
        {
            var result = await _dbContext.GetColection
                                .FindAsync(c => c.Id == Id)
                                .ConfigureAwait(false);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetUsersByMasterId(Guid userMasterId)
        {
            var users = await _dbContext.GetColection.FindAsync(c => c.UserMasterId == userMasterId);

            return users.ToList();
        }

        public async Task<User> Login(string email)
        {
            var builder = Builders<User>.Filter;
            var filter = builder.Eq(user => user.Email, email);

            var result = await _dbContext.GetColection.FindAsync(filter).ConfigureAwait(false);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<bool> RedefinePassword(Guid id, string passwordNew)
        {
            var update = Builders<User>.Update.Set(c => c.Password, passwordNew);
            await _dbContext.GetColection.UpdateOneAsync(c => c.Id == id, update);
            return true;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var update = Builders<User>.Update.Set(c => c.Group, user.Group)
                                              .Set(c => c.UserMasterId, user.UserMasterId)
                                              .Set(c => c.Name, user.Name)
                                              .Set(c => c.Document, user.Document)
                                              .Set(c => c.Email, user.Email)
                                              .Set(c => c.Picture, user.Picture)
                                              .Set(c => c.IsActive, user.IsActive)
                                              .Set(c => c.Companies, user.Companies);

            await _dbContext.GetColection.UpdateOneAsync(c => c.Id == user.Id, update);

            return true;
        }
    }
}
