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
        public string Address { get; set; }

        //[JsonIgnore]
        //public int? Weight { get; set; }

        [JsonIgnore]
        public string Grade { get; set; }
             
        [JsonIgnore]
        public List<Note> InternalNotes { get; set; } = new List<Note>();

        [JsonIgnore]
        public string PermanentNote { get; set; }

        [JsonIgnore]
        public List<string> Tags { get; set; } = new List<string>();

        [JsonIgnore]
        public Nullable<DateTime> LastVisit { get; set; }

        [JsonIgnore]
        public bool CanEdit { get; set; } = false;

        [JsonIgnore]
        public List<CustomerAddressLocation> CustomerAddresses { get; set; }

        [JsonIgnore]
        public List<QuotationsModel> Quotations { get; set; } = new List<QuotationsModel>();

        [JsonIgnore]
        public List<Contract> Contracts { get; set; } = new List<Contract>();


        [JsonProperty("category_id")]
        public long CategoryId { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("commercial_company_name")]
        public string CommercialCompanyName { get; set; }

        [JsonProperty("company_id")]
        public string CompanyId { get; set; }

        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonProperty("contact_address")]
        public string ContactAddress { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("date_localization")]
        public string DateLocalization { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("externalId")]
        public long ExternalId { get; set; }

        [JsonProperty("grade_id")]
        public long GradeId { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parent_id")]
        public long ParentId { get; set; }

        [JsonProperty("parent_name")]
        public string ParentName { get; set; }

        [JsonProperty("partner_latitude")]
        public long PartnerLatitude { get; set; }

        [JsonProperty("partner_longitude")]
        public long PartnerLongitude { get; set; }

        [JsonProperty("partner_weight")]
        public long PartnerWeight { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("title")]
        public long Title { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("weight")]
        public long Weight { get; set; }

        [JsonProperty("write_date")]
        public string WriteDate { get; set; }

    }



}
