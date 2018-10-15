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
        public double AmountTax { get; set; }

        [JsonProperty("amount_total")]
        public double AmountTotal { get; set; }

        [JsonProperty("amount_untaxed")]
        public double AmountUntaxed { get; set; }

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
        public double DeliveryPrice { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("partner_id")]
        public long PartnerId { get; set; }

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

        [JsonProperty("horse_show")]
        public string HorseShow { get; set; }

        [JsonProperty("quotation_name")]
        public string QuotationName { get; set; }

        [JsonProperty("trainer_name")]
        public string TrainerName { get; set; }

        [JsonProperty("payment_note")]
        public string PaymentNote { get; set; }

        [JsonProperty("payment_method")]
        public string PaymentMethod { get; set; }

        [JsonProperty("tax_percent")]
        public double TaxPercent { get; set; }
    }
}
