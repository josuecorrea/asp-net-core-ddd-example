using AspNetCore.Example.Application.Mapping.Response.GetInfomationByDocument;
using MediatR;


namespace AspNetCore.Example.Application.Mapping.Request
{
    public class GetInfomationByDocumentRequest : IRequest<GetInfomationByDocumentResponse>
    {
        public string Document { get; set; }
    }
}
