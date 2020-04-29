using AspNetCore.Example.Domain.Entities.UserAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Example.Application.Mapping.Request
{
    public class ChangeLinkWithTheCompanyRequest:IRequest<string>
    {
        public Guid? Id { get; set; }
        public List<UserCompany> UserCompanies { get; set; }
    }
}
