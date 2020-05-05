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
        private static readonly MongoDBContext<User> _dbContext = new MongoDBContext<User>();

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

            var updateResult =  await _dbContext
                                        .GetColection
                                        .UpdateOneAsync(c => c.Id == user.Id, update)
                                        .ConfigureAwait(false);

            return updateResult.ModifiedCount > 0;
        }

        public async Task DeleteUserById(Guid userId)
        {
            await _dbContext.GetColection
                            .DeleteOneAsync(c => c.Id == userId)
                            .ConfigureAwait(false);
        }

        public async Task<bool> ExistsUser(string userName)
        {
            var result = await _dbContext.GetColection
                                .FindAsync(c => c.Name.Equals(userName))
                                .ConfigureAwait(false);

            return result.Any();
        }

        public async Task<List<UserCompany>> GetAllUserCompany(Guid id)
        {
            var companies = new List<UserCompany>();

            var queryResult = await _dbContext
                                .GetColection
                                .FindAsync(c => c.Id == id)
                                .ConfigureAwait(false);

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
            var users = await _dbContext
                                .GetColection
                                .FindAsync(c => c.UserMasterId == userMasterId)
                                .ConfigureAwait(false);

            return users.ToList();
        }

        public async Task<User> Login(string email)
        {
            var result = await _dbContext
                                .GetColection
                                .FindAsync(c=>c.Email.Equals(email))
                                .ConfigureAwait(false);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<bool> RedefinePassword(Guid id, string passwordNew)
        {
            var update = Builders<User>.Update.Set(c => c.Password, passwordNew);

            var updateResult = await _dbContext
                                       .GetColection
                                       .UpdateOneAsync(c => c.Id == id, update)
                                       .ConfigureAwait(false);

            return updateResult.ModifiedCount > 0;
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

            var updateResult = await _dbContext
                                         .GetColection
                                         .UpdateOneAsync(c => c.Id == user.Id, update)
                                         .ConfigureAwait(false);

            return updateResult.ModifiedCount > 0;
        }
    }
}
