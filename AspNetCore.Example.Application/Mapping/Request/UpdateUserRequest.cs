using AspNetCore.Example.Application.Mapping.Param;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Example.Application.Mapping.Request
{
    public class UpdateUserRequest:NewUserRequest
    {
        public Guid Id { get; set; }
    }
}
