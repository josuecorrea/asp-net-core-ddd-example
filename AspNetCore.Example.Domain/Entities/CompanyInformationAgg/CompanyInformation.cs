using AspNetCore.Example.Domain.Entities.Base;
using System.Collections.Generic;

namespace AspNetCore.Example.Domain.Entities
{
    public class CompanyInformation : Entity
    {
        public string Name { get; set; }

        public string Alias { get; set; }

        public string Cnpj { get; set; }

        public string Type { get; set; }

        public string Founded { get; set; }

        public string Size { get; set; }

        public decimal? Capital { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string FederalEntity { get; set; }       
    }
}
