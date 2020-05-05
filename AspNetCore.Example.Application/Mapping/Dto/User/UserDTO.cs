using AspNetCore.Example.Domain.Entities.UserAgg;
using AspNetCore.Example.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Example.Application.Mapping.Dto.User
{
    public class UserDTO
    {
        public UserGroup Group { get; private set; }
        public Guid? UserMasterId { get; set; }
        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Picture { get; private set; }
        public bool IsActive { get; private set; }

        public List<UserCompany> Companies { get; private set; }
    }
}
