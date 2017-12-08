namespace voltaire.Models.DataObjects
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using voltaire.DataStore.Abstraction;

    public class PurchaseOrder : BaseDataObject
    {
        [JsonProperty("amount_tax")]
        public long AmountTax { get; set; }

        [JsonProperty("amount_total")]
        public long AmountTotal { get; set; }

        [JsonProperty("amount_untaxed")]
        public long AmountUntaxed { get; set; }

        [JsonProperty("bid_date")]
        public string BidDate { get; set; }

        [JsonProperty("bid_validity")]
        public string BidValidity { get; set; }

        [JsonProperty("company_id")]
        public long CompanyId { get; set; }

        [JsonProperty("create_date")]
        public string CreateDate { get; set; }

        [JsonProperty("create_uid")]
        public long CreateUid { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("currency_id")]
        public long CurrencyId { get; set; }

        [JsonProperty("date_approve")]
        public string DateApprove { get; set; }

        [JsonProperty("date_order")]
        public string DateOrder { get; set; }

        [JsonProperty("date_planned")]
        public string DatePlanned { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("externalId")]
        public long ExternalId { get; set; }

        [JsonProperty("internal_partner_id")]
        public string InternalPartnerId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("partner_id")]
        public long PartnerId { get; set; }

        [JsonProperty("picking_type_id")]
        public long PickingTypeId { get; set; }

        [JsonProperty("pricelist_id")]
        public long PricelistId { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("write_date")]
        public string WriteDate { get; set; }
    }
}
