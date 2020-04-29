using AspNetCore.Example.Infra.Repositories.Base;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AspNetCore.Example.Infra.Models.CompanyInformation
{
    public class CompanyInformationModel : HttpBaseResult
    {     

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("tax_id")]
        public string Cnpj { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("founded")]
        public string Founded { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("capital")]
        public decimal? Capital { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("federal_entity")]
        public string FederalEntity { get; set; }       
    }
}
