namespace voltaire.Models.DataObjects
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using voltaire.DataStore.Abstraction;

    public class SaleOrderLine : BaseDataObject
    {
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

        [JsonProperty("date_planned")]
        public string DatePlanned { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("discount")]
        public long Discount { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("externalId")]
        public long ExternalId { get; set; }

        [JsonProperty("internal_product_id")]
        public string InternalProductId { get; set; }

        [JsonProperty("invoice_status")]
        public string InvoiceStatus { get; set; }

        [JsonProperty("invoiced")]
        public bool Invoiced { get; set; }

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

        [JsonProperty("price_total")]
        public long PriceTotal { get; set; }

        [JsonProperty("price_unit")]
        public long PriceUnit { get; set; }

        [JsonProperty("product_id")]
        public long ProductId { get; set; }

        [JsonProperty("product_qty")]
        public long ProductQty { get; set; }

        [JsonProperty("product_uom")]
        public long ProductUom { get; set; }

        [JsonProperty("product_uom_qty")]
        public long ProductUomQty { get; set; }

        [JsonProperty("product_uos")]
        public long ProductUos { get; set; }

        [JsonProperty("product_uos_qty")]
        public long ProductUosQty { get; set; }

        [JsonProperty("qty_delivered")]
        public long QtyDelivered { get; set; }

        [JsonProperty("qty_delivered_updateable")]
        public bool QtyDeliveredUpdateable { get; set; }

        [JsonProperty("qty_invoiced")]
        public long QtyInvoiced { get; set; }

        [JsonProperty("qty_to_invoice")]
        public long QtyToInvoice { get; set; }

        [JsonProperty("salesman_id")]
        public long SalesmanId { get; set; }

        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("th_weight")]
        public long ThWeight { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("warehouse_id")]
        public long WarehouseId { get; set; }

        [JsonProperty("write_date")]
        public string WriteDate { get; set; }
    }
}
