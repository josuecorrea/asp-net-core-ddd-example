using AspNetCore.Example.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AspNetCore.Example.Infra.Repositories
{
    public class UserContextAcessorRepository : IUserContextAcessorRepository
    {
        private readonly IHttpContextAccessor _accessor;

        public UserContextAcessorRepository(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;
        public string Id => GetClaimsIdentity().FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

    }
}
