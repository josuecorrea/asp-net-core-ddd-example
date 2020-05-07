using AspNetCore.Example.Domain.Enum;

namespace AspNetCore.Example.Application.Mapping.Dto.User
{
    public class GetUserDTO
    {
        public GetUserDTO(UserGroup group, string name, string document, string email, string picture, bool isActive)
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