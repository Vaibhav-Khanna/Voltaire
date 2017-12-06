using System;
using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;
using voltaire.DataStore.Abstraction;

namespace voltaire.Models
{

    //public class Partner : BaseDataObject
    //{

    //    public string Name { get; set; }

    //    public string Address { get; set; }

    //    public string Status { get; set; }

    //    public string Phone { get; set; }

    //    public string CompanyName { get; set; }

    //    public string Email { get; set; }

    //    public int? Weight { get; set; }

    //    public string Grade { get; set; }

    //    public string Website { get; set; }

    //    public List<Note> InternalNotes { get; set; } = new List<Note>();

    //    public string PermanentNote { get; set; }

    //    public List<string> Tags { get; set; } = new List<string>();

    //    public Nullable<DateTime> LastVisit { get; set; }

    //    public bool CanEdit { get; set; } = false;

    //    public List<CustomerAddressLocation> CustomerAddresses { get; set; }

    //    public List<QuotationsModel> Quotations { get; set; } = new List<QuotationsModel>();

    //    public List<Contract> Contracts { get; set; } = new List<Contract>();

    //}


    public class Partner : BaseDataObject
    {
        
        [JsonIgnore]
        public string Address { get; set; }

        [JsonIgnore]
        public int? Weight { get; set; }

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


        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("activities_count")]
        public long ActivitiesCount { get; set; }

        [JsonProperty("category_id")]
        public long CategoryId { get; set; }

        [JsonProperty("channel_ids")]
        public string ChannelIds { get; set; }

        [JsonProperty("child_ids")]
        public string ChildIds { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("color")]
        public long Color { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("commercial_company_name")]
        public string CommercialCompanyName { get; set; }

        [JsonProperty("commercial_partner_id")]
        public string CommercialPartnerId { get; set; }

        [JsonProperty("company_id")]
        public long CompanyId { get; set; }

        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonProperty("company_type")]
        public string CompanyType { get; set; }

        [JsonProperty("contact_address")]
        public string ContactAddress { get; set; }

        [JsonProperty("contract_ids")]
        public string ContractIds { get; set; }

        [JsonProperty("contracts_count")]
        public long ContractsCount { get; set; }

        [JsonProperty("country_id")]
        public long CountryId { get; set; }

        [JsonProperty("create_date")]
        public string CreateDate { get; set; }

        [JsonProperty("create_uid")]
        public long CreateUid { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("credit")]
        public long Credit { get; set; }

        [JsonProperty("currency_id")]
        public long CurrencyId { get; set; }

        [JsonProperty("customer")]
        public bool Customer { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("debit")]
        public long Debit { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("email_formatted")]
        public string EmailFormatted { get; set; }

        [JsonProperty("employee")]
        public bool Employee { get; set; }

        [JsonProperty("externalId")]
        public long ExternalId { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("function")]
        public string Function { get; set; }

        [JsonProperty("grade_id")]
        public long GradeId { get; set; }

        //[JsonProperty("id")]
        //public string Id { get; set; }

        [JsonProperty("invoice_ids")]
        public string InvoiceIds { get; set; }

        [JsonProperty("is_company")]
        public bool IsCompany { get; set; }

        [JsonProperty("journal_item_count")]
        public long JournalItemCount { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("message_follower_ids")]
        public string MessageFollowerIds { get; set; }

        [JsonProperty("message_needaction")]
        public bool MessageNeedaction { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("notify_email")]
        public string NotifyEmail { get; set; }

        [JsonProperty("opportunity_count")]
        public long OpportunityCount { get; set; }

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

        [JsonProperty("picking_warn")]
        public string PickingWarn { get; set; }

        [JsonProperty("picking_warn_msg")]
        public string PickingWarnMsg { get; set; }

        [JsonProperty("property_account_position_id")]
        public long PropertyAccountPositionId { get; set; }

        [JsonProperty("property_payment_term_id")]
        public long PropertyPaymentTermId { get; set; }

        [JsonProperty("property_product_pricelist")]
        public long PropertyProductPricelist { get; set; }

        [JsonProperty("ref")]
        public string Ref { get; set; }

        [JsonProperty("sale_order_count")]
        public long SaleOrderCount { get; set; }

        [JsonProperty("sale_order_ids")]
        public string SaleOrderIds { get; set; }

        [JsonProperty("sale_warn")]
        public string SaleWarn { get; set; }

        [JsonProperty("section_id")]
        public long SectionId { get; set; }

        [JsonProperty("signup_type")]
        public string SignupType { get; set; }

        [JsonProperty("state_id")]
        public long StateId { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("street2")]
        public string Street2 { get; set; }

        [JsonProperty("supplier")]
        public bool Supplier { get; set; }

        [JsonProperty("team_id")]
        public long TeamId { get; set; }

        [JsonProperty("title")]
        public long Title { get; set; }

        [JsonProperty("total_invoiced")]
        public long TotalInvoiced { get; set; }

        [JsonProperty("trust")]
        public string Trust { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("user_ids")]
        public string UserIds { get; set; }

        [JsonProperty("vat")]
        public string Vat { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("write_date")]
        public string WriteDate { get; set; }

        [JsonProperty("write_uid")]
        public string WriteUid { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

    }



}
