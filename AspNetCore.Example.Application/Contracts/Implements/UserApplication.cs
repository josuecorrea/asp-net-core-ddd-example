using AspNetCore.Example.Application.Contracts.Repositories;
using AspNetCore.Example.Application.Mapping.Result.GenerateAccessToken;
using AspNetCore.Example.Domain.Contracts.Repositories;
using AspNetCore.Example.Domain.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Example.Application.Contracts.Implements
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private const int QuantityDaysForTokenExpiration = 1;

        public UserApplication(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<GenerateAccessTokenResponse> GenerateAccessToken(GenerateAccessTokenFilter generateAccessTokenFilter)
        {
            var user = await _userRepository.Login(generateAccessTokenFilter.Email);           

            var passwordVerifacation = user.ValidatePassword(generateAccessTokenFilter.Password);
            if (!passwordVerifacation)
                return new GenerateAccessTokenResponse(false);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Group.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(QuantityDaysForTokenExpiration),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var writeToken = tokenHandler.WriteToken(token);

            return new GenerateAccessTokenResponse(user, true, writeToken);
        }
    }
}
