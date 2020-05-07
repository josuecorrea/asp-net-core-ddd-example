using AspNetCore.Example.Application.Mapping.Param;
using System;

namespace AspNetCore.Example.Application.Mapping.Request
{
    public class UpdateUserRequest : NewUserRequest
    {
        public Guid Id { get; set; }
    }
}
