using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AspNetCore.Example.Application.Mapping.Request
{
    public class RedefinePasswordRequest:IRequest<string>
    {
        public Guid? Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }


        public void GeneratePassword()
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(NewPassword));
                NewPassword = Encoding.ASCII.GetString(result);
            }
        }
    }
}
