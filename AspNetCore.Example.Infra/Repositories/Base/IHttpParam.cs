namespace AspNetCore.Example.Infra.Repositories.Base
{
    public interface IHttpParam
    {
        string GetApiUrl();
        string GetToken();
        bool IsAuthentication();
    }
}
