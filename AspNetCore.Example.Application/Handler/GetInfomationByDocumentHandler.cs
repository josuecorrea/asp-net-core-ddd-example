using AutoMapper;
using AspNetCore.Example.Application.Mapping.Request;
using AspNetCore.Example.Application.Mapping.Result.GetInfomationByDocument;
using AspNetCore.Example.Domain.Entities;
using AspNetCore.Example.Infra.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCore.Example.Application.Handler
{
    public class GetInfomationByDocumentHandler : IRequestHandler<GetInfomationByDocumentRequest, GetInfomationByDocumentResponse>
    {
        private readonly ICompanyGateway _companyGateway;
        private readonly IMapper _mapper;

        public GetInfomationByDocumentHandler(ICompanyGateway companyGateway, IMapper mapper)
        {
            _companyGateway = companyGateway;
            _mapper = mapper;
        }

        public async Task<GetInfomationByDocumentResponse> Handle(GetInfomationByDocumentRequest request, CancellationToken cancellationToken)
        {
            var companyInformation = await _companyGateway.GetCompanyInformationByCnpj(request.Document);
            
            return new GetInfomationByDocumentResponse(_mapper.Map<CompanyInformationDto>(companyInformation));
        }
    }
}
