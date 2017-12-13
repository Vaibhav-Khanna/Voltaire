namespace voltaire.Models.DataObjects
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using voltaire.DataStore.Abstraction;

    public class SaleOrderLine : BaseDataObject
    {
      
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("externalId")]
        public long ExternalId { get; set; }

        [JsonProperty("write_date")]
        public DateTime WriteDate { get; set; }

        [JsonProperty("company_id")]
        public long CompanyId { get; set; }

        [JsonProperty("create_date")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("create_uid")]
        public long CreateUid { get; set; }

        [JsonProperty("currency_id")]
        public long CurrencyId { get; set; }

        [JsonProperty("discount")]
        public long Discount { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("is_delivery")]
        public bool IsDelivery { get; set; }

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

        [JsonProperty("product_id")]
        public long ProductId { get; set; }

        [JsonProperty("product_qty")]
        public long ProductQty { get; set; }

        [JsonProperty("product_uom")]
        public long ProductUom { get; set; }

        [JsonProperty("product_uom_qty")]
        public long ProductUomQty { get; set; }

        [JsonProperty("salesman_id")]
        public long SalesmanId { get; set; }

        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
