using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Example.Application.Mapping.Request
{
    public class DeleteUserRequest:IRequest<string>
    {
        public Guid? Id { get; set; }
    }
}
