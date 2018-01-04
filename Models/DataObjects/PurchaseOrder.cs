namespace voltaire.Models.DataObjects
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using voltaire.DataStore.Abstraction;

    public class PurchaseOrder : BaseDataObject
    {
       
        //[JsonProperty("version")]
        //public string Version { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("amount_tax")]
        public long AmountTax { get; set; }

        [JsonProperty("amount_total")]
        public long AmountTotal { get; set; }

        [JsonProperty("amount_untaxed")]
        public long AmountUntaxed { get; set; }

        [JsonProperty("bid_date")]
        public DateTime BidDate { get; set; }

        [JsonProperty("bid_validity")]
        public DateTime BidValidity { get; set; }

        [JsonProperty("company_id")]
        public long CompanyId { get; set; }

        [JsonProperty("create_date")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("create_uid")]
        public long CreateUid { get; set; }

        [JsonProperty("currency_id")]
        public long CurrencyId { get; set; }

        [JsonProperty("date_approve")]
        public DateTime DateApprove { get; set; }

        [JsonProperty("date_planned")]
        public DateTime DatePlanned { get; set; }

        [JsonProperty("date_order")]
        public DateTime DateOrder { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("partner_id")]
        public long PartnerId { get; set; }

        [JsonProperty("internal_partner_id")]
        public string InternalPartnerId { get; set; }

        [JsonProperty("picking_type_id")]
        public long PickingTypeId { get; set; }

        [JsonProperty("pricelist_id")]
        public long PricelistId { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("write_date")]
        public DateTime WriteDate { get; set; }

        [JsonProperty("externalId")]
        public long ExternalId { get; set; }
    }
}
