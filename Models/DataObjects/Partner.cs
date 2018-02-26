using System;
using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;
using voltaire.DataStore.Abstraction;

namespace voltaire.Models
{

    public class Partner : BaseDataObject
    {
      
        [JsonIgnore]
        public List<Note> InternalNotes { get; set; } = new List<Note>();

        [JsonIgnore]
        public List<string> Tags { get; set; } = new List<string>();

        [JsonIgnore]
        public bool CanEdit { get; set; } = false;

        [JsonIgnore]
        public List<CustomerAddressLocation> CustomerAddresses { get; set; }

        [JsonIgnore]
        public List<QuotationsModel> Quotations { get; set; } = new List<QuotationsModel>();

        [JsonIgnore]
        public List<Contract> Contracts { get; set; } = new List<Contract>();


        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("external_id")]
        public long ExternalId { get; set; }

        [JsonProperty("write_date")]
        public DateTime WriteDate { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("contact_address")]
        public string ContactAddress { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("partner_weight")]
        public long PartnerWeight { get; set; }

        [JsonProperty("weight")]
        public long Weight { get; set; }

        [JsonProperty("grade_id")]
        public long GradeId { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("category_id")]
        public long CategoryId { get; set; }

        [JsonProperty("company_id")]
        public string CompanyId { get; set; }

        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonProperty("commercial_company_name")]
        public string CommercialCompanyName { get; set; }

        [JsonProperty("partner_longitude")]
        public double PartnerLongitude { get; set; }

        [JsonProperty("partner_latitude")]
        public double PartnerLatitude { get; set; }

        [JsonProperty("date_localization")]
        public DateTime? DateLocalization { get; set; }

        [JsonProperty("parent_id")]
        public long ParentId { get; set; }

        [JsonProperty("parent_name")]
        public string ParentName { get; set; }

        [JsonProperty("title")]
        public long Title { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("currency_id")]
        public long CurrencyId { get; set; }

        [JsonProperty("property_product_pricelist")]
        public long PropertyProductPricelist { get; set; }

        [JsonProperty("last_checkin_at")]
        public DateTime? LastCheckinAt { get; set; }
    }
}
