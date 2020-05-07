using MediatR;


namespace AspNetCore.Example.Application.Mapping.Request
{
    public class GetInfomationByDocumentRequest : IRequest<Response.Response>
    {
        public string Document { get; set; }
    }
}
