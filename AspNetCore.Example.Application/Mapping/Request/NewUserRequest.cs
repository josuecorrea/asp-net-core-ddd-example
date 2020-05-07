using AspNetCore.Example.Domain.Entities.UserAgg;
using AspNetCore.Example.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;

namespace AspNetCore.Example.Application.Mapping.Param
{
    public class NewUserRequest : IRequest<Response.Response>
    {
        public NewUserRequest()
        {
            Companies = new List<UserCompany>();
            IsActive = true;
        }

        public UserGroup Group { get; set; }
        public Guid? UserMasterId { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Picture { get; set; }
        public bool IsActive { get; set; }

        //TODO: essas propriedades devem virar value objects
        public List<UserCompany> Companies { get; set; }
    }
}
