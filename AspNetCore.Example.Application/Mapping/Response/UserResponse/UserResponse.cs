using AspNetCore.Example.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Example.Application.Mapping.Response.UserResponse
{
    public class UserResponse:BaseResult
    {
        public UserResponse()
        {

        }
        public UserResponse(string error)
        {
            AddMessageError(error);
        }

        public UserResponse(List<string> errors)
        {
            Erros = errors;
        }

        public void AddUserMessageError(string error)
        {
            AddMessageError(error);
        }
    }
}
