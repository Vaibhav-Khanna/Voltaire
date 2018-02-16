namespace voltaire.Models.DataObjects
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using voltaire.DataStore.Abstraction;

    public class PurchaseOrderLine : BaseDataObject
    {
      
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("company_id")]
        public long CompanyId { get; set; }

        [JsonProperty("create_date")]
        public System.DateTime CreateDate { get; set; }

        [JsonProperty("create_uid")]
        public long CreateUid { get; set; }

        [JsonProperty("currency_id")]
        public long CurrencyId { get; set; }

        [JsonProperty("date_planned")]
        public System.DateTime DatePlanned { get; set; }

        [JsonProperty("invoiced")]
        public bool Invoiced { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        [JsonProperty("price_subtotal")]
        public long PriceSubtotal { get; set; }

        [JsonProperty("price_tax")]
        public long PriceTax { get; set; }

        [JsonProperty("price_unit")]
        public long PriceUnit { get; set; }

        [JsonProperty("price_total")]
        public long PriceTotal { get; set; }

        [JsonProperty("external_product_id")]
        public long ExternalProductId { get; set; }

        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("product_qty")]
        public long ProductQty { get; set; }

        [JsonProperty("product_uom")]
        public long ProductUom { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("write_date")]
        public System.DateTime WriteDate { get; set; }

        [JsonProperty("external_id")]
        public long ExternalId { get; set; }
    }
}
