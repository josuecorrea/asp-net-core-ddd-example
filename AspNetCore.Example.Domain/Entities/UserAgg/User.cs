using AspNetCore.Example.Domain.Entities.Base;
using AspNetCore.Example.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AspNetCore.Example.Domain.Entities.UserAgg
{
    public class User : Entity
    {
        public User()
        {

        }
        public User(Guid Id) {
            this.Id = Id;
        }

        public User(UserGroup group, string name, string document, string email,
                    string password, string picture, bool isActive, List<UserCompany> companies, 
                    Guid? userMasterId)
        {
            Group = group;
            Name = name;
            Document = document;
            Email = email;
            Password = password;
            Picture = picture;
            IsActive = isActive;
            InsertDate = DateTime.Now;
            Companies = companies;
            UserMasterId = userMasterId;

            SetPasswordHash();
        }

        public UserGroup Group { get; private set; }
        public Guid? UserMasterId { get; set; }
        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Picture { get; private set; }
        public bool IsActive { get; private set; }

        public List<UserCompany> Companies { get; private set; }

        public DateTime InsertDate { get; private set; }

        public void SetPasswordHash()
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(Password));
                Password = Encoding.ASCII.GetString(result);
            }
        }

        public bool ValidatePassword(string password)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
                password = Encoding.ASCII.GetString(result);

                return password.Equals(Password);
            }
        }
    }
}
