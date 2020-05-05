using System.Collections.Generic;
using System.Security.Claims;

namespace AspNetCore.Example.Infra.Repositories.Interfaces
{
    public interface IUserContextAcessorRepository
    {
        string Name { get; }
        string Id { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
