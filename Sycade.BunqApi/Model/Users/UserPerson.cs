using Newtonsoft.Json;
using System;

namespace Sycade.BunqApi.Model.Users
{
    public class UserPerson : User
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("middle_name")]
        public string MiddleName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("legal_name")]
        public string LegalName { get; set; }
        [JsonProperty("tax_resident")]
        public string TaxResident { get; set; }

        [JsonProperty("date_of_birth")]
        public DateTime DateOfBirth { get; set; }
        [JsonProperty("place_of_birth")]
        public string PlaceOfBirth { get; set; }
        [JsonProperty("country_of_birth")]
        public string CountryOfBirth { get; set; }

        [JsonProperty("nationality")]
        public string Nationality { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("document_number")]
        public string DocumentNumber { get; set; }
        [JsonProperty("document_type")]
        public string DocumentType { get; set; }
        [JsonProperty("document_country_of_issuance")]
        public string DocumentCountryOfIssuance { get; set; }
        [JsonProperty("document_front_attachment")]
        public Attachment DocumentFrontAttachment { get; set; }
        [JsonProperty("document_back_attachment")]
        public Attachment DocumentBackAttachment { get; set; }
    }
}
