namespace voltaire.Models.DataObjects
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using voltaire.DataStore.Abstraction;

    public class SaleOrderLine : BaseDataObject
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
        public System.DateTime WriteDate { get; set; }

        [JsonProperty("company_id")]
        public long CompanyId { get; set; }

        [JsonProperty("create_date")]
        public System.DateTime CreateDate { get; set; }

        [JsonProperty("create_uid")]
        public long CreateUid { get; set; }

        [JsonProperty("currency_id")]
        public long CurrencyId { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("is_delivery")]
        public bool IsDelivery { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("price_unit")]
        public double PriceUnit { get; set; }

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

        [JsonProperty("tax_id")]
        public long TaxId { get; set; }

        [JsonProperty("configuration_detail")]
        public string ConfigurationDetail { get; set; }

        [JsonProperty("product_kind")]
        public string ProductKind { get; set; }

        [JsonProperty("tax_applied")]
        public bool TaxApplied { get; set; }
    }

    public enum ProductKind
    {
        saddle, accessory, other, service, discount, tradein
    }

}
