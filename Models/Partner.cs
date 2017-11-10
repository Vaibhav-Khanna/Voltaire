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
        public string Status { get; set; }

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

        [JsonProperty("bank_account_count")]
        public long BankAccountCount { get; set; }

        [JsonProperty("bank_ids")]
        public string BankIds { get; set; }

        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("calendar_last_notif_ack")]
        public string CalendarLastNotifAck { get; set; }

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
        public bool CompanyType { get; set; }

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

        [JsonProperty("credit_limit")]
        public long CreditLimit { get; set; }

        [JsonProperty("currency_id")]
        public long CurrencyId { get; set; }

        [JsonProperty("customer")]
        public bool Customer { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("debit")]
        public long Debit { get; set; }

        [JsonProperty("debit_limit")]
        public long DebitLimit { get; set; }

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

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("function")]
        public string Function { get; set; }

        [JsonProperty("has_unreconciled_entries")]
        public bool HasUnreconciledEntries { get; set; }

        //[JsonProperty("id")]
        //public string id { get; set; }

        [JsonProperty("im_status")]
        public string ImStatus { get; set; }

        [JsonProperty("invoice_ids")]
        public string InvoiceIds { get; set; }

        [JsonProperty("invoice_warn")]
        public bool InvoiceWarn { get; set; }

        [JsonProperty("invoice_warn_msg")]
        public string InvoiceWarnMsg { get; set; }

        [JsonProperty("is_company")]
        public bool IsCompany { get; set; }

        [JsonProperty("issued_total")]
        public long IssuedTotal { get; set; }

        [JsonProperty("journal_item_count")]
        public long JournalItemCount { get; set; }

        [JsonProperty("lang")]
        public bool Lang { get; set; }

        [JsonProperty("last_time_entries_checked")]
        public string LastTimeEntriesChecked { get; set; }

        [JsonProperty("__last_update")]
        public string LastUpdate { get; set; }

        [JsonProperty("meeting_count")]
        public long MeetingCount { get; set; }

        [JsonProperty("meeting_ids")]
        public string MeetingIds { get; set; }

        [JsonProperty("message_bounce")]
        public long MessageBounce { get; set; }

        [JsonProperty("message_channel_ids")]
        public string MessageChannelIds { get; set; }

        [JsonProperty("message_follower_ids")]
        public string MessageFollowerIds { get; set; }

        [JsonProperty("message_ids")]
        public string MessageIds { get; set; }

        [JsonProperty("message_is_follower")]
        public bool MessageIsFollower { get; set; }

        [JsonProperty("message_last_post")]
        public string MessageLastPost { get; set; }

        [JsonProperty("message_needaction")]
        public bool MessageNeedaction { get; set; }

        [JsonProperty("message_needaction_counter")]
        public long MessageNeedactionCounter { get; set; }

        [JsonProperty("message_partner_ids")]
        public string MessagePartnerIds { get; set; }

        [JsonProperty("message_unread")]
        public bool MessageUnread { get; set; }

        [JsonProperty("message_unread_counter")]
        public long MessageUnreadCounter { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = "Bill Goldberg";

        [JsonProperty("notify_email")]
        public bool NotifyEmail { get; set; }

        [JsonProperty("opportunity_count")]
        public long OpportunityCount { get; set; }

        [JsonProperty("opportunity_ids")]
        public string OpportunityIds { get; set; }

        [JsonProperty("opt_out")]
        public bool OptOut { get; set; }

        [JsonProperty("parent_id")]
        public long ParentId { get; set; }

        [JsonProperty("parent_name")]
        public string ParentName { get; set; }

        [JsonProperty("partner_share")]
        public bool PartnerShare { get; set; }

        [JsonProperty("payment_token_count")]
        public long PaymentTokenCount { get; set; }

        [JsonProperty("payment_token_ids")]
        public string PaymentTokenIds { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("picking_warn")]
        public bool PickingWarn { get; set; }

        [JsonProperty("picking_warn_msg")]
        public string PickingWarnMsg { get; set; }

        [JsonProperty("property_account_payable_id")]
        public long PropertyAccountPayableId { get; set; }

        [JsonProperty("property_account_position_id")]
        public long PropertyAccountPositionId { get; set; }

        [JsonProperty("property_account_receivable_id")]
        public long PropertyAccountReceivableId { get; set; }

        [JsonProperty("property_payment_term_id")]
        public long PropertyPaymentTermId { get; set; }

        [JsonProperty("property_product_pricelist")]
        public long PropertyProductPricelist { get; set; }

        [JsonProperty("property_stock_customer")]
        public long PropertyStockCustomer { get; set; }

        [JsonProperty("property_stock_supplier")]
        public long PropertyStockSupplier { get; set; }

        [JsonProperty("property_supplier_payment_term_id")]
        public long PropertySupplierPaymentTermId { get; set; }

        [JsonProperty("ref")]
        public string Ref { get; set; }

        [JsonProperty("ref_company_ids")]
        public string RefCompanyIds { get; set; }

        [JsonProperty("sale_order_count")]
        public long SaleOrderCount { get; set; }

        [JsonProperty("sale_order_ids")]
        public string SaleOrderIds { get; set; }

        [JsonProperty("sale_warn")]
        public bool SaleWarn { get; set; }

        [JsonProperty("sale_warn_msg")]
        public string SaleWarnMsg { get; set; }

        [JsonProperty("self")]
        public long Self { get; set; }

        [JsonProperty("signup_expiration")]
        public string SignupExpiration { get; set; }

        [JsonProperty("signup_token")]
        public string SignupToken { get; set; }

        [JsonProperty("signup_type")]
        public string SignupType { get; set; }

        [JsonProperty("signup_url")]
        public string SignupUrl { get; set; }

        [JsonProperty("signup_valid")]
        public bool SignupValid { get; set; }

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
        public bool Trust { get; set; }

        [JsonProperty("type")]
        public bool Type { get; set; }

        [JsonProperty("tz")]
        public bool Tz { get; set; }

        [JsonProperty("tz_offset")]
        public string TzOffset { get; set; }

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
