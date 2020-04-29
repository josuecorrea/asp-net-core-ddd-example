namespace AspNetCore.Example.Infra.Repositories.Base
{
    public sealed class HttpParam : IHttpParam
    { 
        public HttpParam(string apiUrl, string token, bool isAuthentication)
        {
            ApiUrl = apiUrl;
            Token = token;
            Authentication = isAuthentication;
        }

        public string ApiUrl { get; private set; }

        public string Token { get; private set; }

        public bool Authentication { get; private set; }

        public string GetApiUrl() => ApiUrl;

        public string GetToken() => Token;

        public bool IsAuthentication() => Authentication;
    }
}
