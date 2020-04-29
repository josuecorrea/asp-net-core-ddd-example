using AspNetCore.Example.Application.Mapping.Result.GenerateAccessToken;
using AspNetCore.Example.Domain.Filters;
using System.Threading.Tasks;

namespace AspNetCore.Example.Application.Contracts.Repositories
{
    public interface IUserApplication
    {
        Task<GenerateAccessTokenResponse> GenerateAccessToken(GenerateAccessTokenFilter validateLoginFilter);
    }
}
