using System;

namespace AspNetCore.Example.Domain.Entities.UserAgg
{
    public class UserCompany
    {
        public string Document { get; set; }
        public DateTime InsertDate { get; private set; } = DateTime.Now;
    }
}
