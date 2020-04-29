using AspNetCore.Example.Domain.Entities.UserAgg;

namespace AspNetCore.Example.Application.Mapping.Result.GenerateAccessToken
{
    public class GenerateAccessTokenResponse
    {
        public GenerateAccessTokenResponse(bool authorized)
        {
            Authorized = authorized;
        }

        public GenerateAccessTokenResponse(User user, bool authorized, string token)
        {
            User = user;
            Authorized = authorized;
            Token = token;
        }

        public User User { get; set; }
        public bool Authorized { get; set; }
        public string Token { get; set; }
    }
}
