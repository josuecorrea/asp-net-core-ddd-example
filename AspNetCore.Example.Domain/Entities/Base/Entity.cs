using System;
using System.Collections.Generic;

namespace AspNetCore.Example.Domain.Entities.Base
{
    public class Entity : ValueObject
    {
        public Guid? Id { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
