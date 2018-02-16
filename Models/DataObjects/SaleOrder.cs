namespace voltaire.Models.DataObjects
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using voltaire.DataStore.Abstraction;

    public class SaleOrder : BaseDataObject
    {
       
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

        [JsonProperty("all_qty_delivered")]
        public bool AllQtyDelivered { get; set; }

        [JsonProperty("amount_tax")]
        public long AmountTax { get; set; }

        [JsonProperty("amount_total")]
        public long AmountTotal { get; set; }

        [JsonProperty("amount_untaxed")]
        public long AmountUntaxed { get; set; }

        [JsonProperty("client_order_ref")]
        public string ClientOrderRef { get; set; }

        [JsonProperty("company_id")]
        public long CompanyId { get; set; }

        [JsonProperty("create_date")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("create_uid")]
        public long CreateUid { get; set; }

        [JsonProperty("currency_id")]
        public long CurrencyId { get; set; }

        [JsonProperty("date_order")]
        public DateTime DateOrder { get; set; }

        [JsonProperty("delivery_count")]
        public long DeliveryCount { get; set; }

        [JsonProperty("delivery_price")]
        public long DeliveryPrice { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("partner_id")]
        public long PartnerId { get; set; }

        [JsonProperty("partner_invoice_id")]
        public long PartnerInvoiceId { get; set; }

        [JsonProperty("partner_shipping_id")]
        public long PartnerShippingId { get; set; }

        [JsonProperty("pricelist_id")]
        public long PricelistId { get; set; }

        [JsonProperty("shipped")]
        public bool Shipped { get; set; }

        [JsonProperty("team_id")]
        public long TeamId { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("to_send")]
        public bool ToSend { get; set; }

        [JsonProperty("file_uploaded")]
        public bool FileUploaded { get; set; }
    }
}
