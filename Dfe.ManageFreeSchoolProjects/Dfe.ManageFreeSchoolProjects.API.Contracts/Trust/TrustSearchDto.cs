using Newtonsoft.Json;
namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Trust
{
    public class TrustSearchDto
    {
        [JsonProperty("ukprn")]
        public virtual string UkPrn { get; set; }

        [JsonProperty("urn")]
        public virtual string Urn { get; set; }

        [JsonProperty("groupName")]
        public virtual string GroupName { get; set; }

        [JsonProperty("companiesHouseNumber")]
        public virtual string CompaniesHouseNumber { get; set; }

        [JsonProperty("trustType")]
        public virtual string TrustType { get; set; }


        [JsonConstructor]
        public TrustSearchDto(string ukprn, string urn, string groupName,
            string companiesHouseNumber, string trustType) =>
            (UkPrn, Urn, GroupName, CompaniesHouseNumber, TrustType) =
            (ukprn, urn, groupName, companiesHouseNumber, trustType);

        public TrustSearchDto() { }
    }
}
