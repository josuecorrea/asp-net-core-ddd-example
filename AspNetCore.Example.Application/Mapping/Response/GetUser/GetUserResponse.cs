using AspNetCore.Example.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Example.Application.Mapping.Result.GetUser
{
    public sealed class GetUserResponse
    {
        public GetUserResponse(UserGroup group, string name, string document, string email, string picture, bool isActive)
        {
            Group = group;
            Name = name;
            Document = document;
            Email = email;
            Picture = picture;
            IsActive = isActive;
        }

        public UserGroup Group { get; private set; }
        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Email { get; private set; }
        public string Picture { get; private set; }
        public bool IsActive { get; private set; }
    }
}
