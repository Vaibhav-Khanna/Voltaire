namespace voltaire.Models.DataObjects
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using voltaire.DataStore.Abstraction;

    public class SaleOrder : BaseDataObject
    {
        [JsonProperty("all_qty_delivered")]
        public bool AllQtyDelivered { get; set; }

        [JsonProperty("amount_tax")]
        public long AmountTax { get; set; }

        [JsonProperty("amount_total")]
        public long AmountTotal { get; set; }

        [JsonProperty("amount_untaxed")]
        public long AmountUntaxed { get; set; }

        [JsonProperty("campaign_id")]
        public long CampaignId { get; set; }

        [JsonProperty("carrier_id")]
        public long CarrierId { get; set; }

        [JsonProperty("client_order_ref")]
        public string ClientOrderRef { get; set; }

        [JsonProperty("company_id")]
        public long CompanyId { get; set; }

        [JsonProperty("confirmation_date")]
        public string ConfirmationDate { get; set; }

        [JsonProperty("create_date")]
        public string CreateDate { get; set; }

        [JsonProperty("create_uid")]
        public long CreateUid { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("currency_id")]
        public long CurrencyId { get; set; }

        [JsonProperty("date_confirm")]
        public string DateConfirm { get; set; }

        [JsonProperty("date_order")]
        public string DateOrder { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("delivery_count")]
        public long DeliveryCount { get; set; }

        [JsonProperty("delivery_price")]
        public long DeliveryPrice { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("externalId")]
        public long ExternalId { get; set; }

        [JsonProperty("fiscal_position_id")]
        public long FiscalPositionId { get; set; }

        [JsonProperty("invoice_count")]
        public long InvoiceCount { get; set; }

        [JsonProperty("invoice_exists")]
        public bool InvoiceExists { get; set; }

        [JsonProperty("invoice_quantity")]
        public long InvoiceQuantity { get; set; }

        [JsonProperty("invoice_shipping_on_delivery")]
        public bool InvoiceShippingOnDelivery { get; set; }

        [JsonProperty("invoiced")]
        public bool Invoiced { get; set; }

        [JsonProperty("invoiced_rate")]
        public long InvoicedRate { get; set; }

        [JsonProperty("medium_id")]
        public long MediumId { get; set; }

        [JsonProperty("message_is_follower")]
        public bool MessageIsFollower { get; set; }

        [JsonProperty("message_last_post")]
        public string MessageLastPost { get; set; }

        [JsonProperty("message_needaction")]
        public bool MessageNeedaction { get; set; }

        [JsonProperty("message_needaction_counter")]
        public long MessageNeedactionCounter { get; set; }

        [JsonProperty("message_summary")]
        public string MessageSummary { get; set; }

        [JsonProperty("message_unread")]
        public bool MessageUnread { get; set; }

        [JsonProperty("message_unread_counter")]
        public long MessageUnreadCounter { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("opportunity_id")]
        public long OpportunityId { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("partner_id")]
        public long PartnerId { get; set; }

        [JsonProperty("partner_invoice_id")]
        public long PartnerInvoiceId { get; set; }

        [JsonProperty("partner_shipping_id")]
        public long PartnerShippingId { get; set; }

        [JsonProperty("payment_mode_id")]
        public long PaymentModeId { get; set; }

        [JsonProperty("payment_term_id")]
        public long PaymentTermId { get; set; }

        [JsonProperty("paypal_url")]
        public string PaypalUrl { get; set; }

        [JsonProperty("picked_rate")]
        public long PickedRate { get; set; }

        [JsonProperty("pricelist_id")]
        public long PricelistId { get; set; }

        [JsonProperty("procurement_group_id")]
        public long ProcurementGroupId { get; set; }

        [JsonProperty("product_id")]
        public long ProductId { get; set; }

        [JsonProperty("project_id")]
        public long ProjectId { get; set; }

        [JsonProperty("project_project_id")]
        public long ProjectProjectId { get; set; }

        [JsonProperty("related_project_id")]
        public long RelatedProjectId { get; set; }

        [JsonProperty("shipped")]
        public bool Shipped { get; set; }

        [JsonProperty("shop_id")]
        public long ShopId { get; set; }

        [JsonProperty("source_id")]
        public long SourceId { get; set; }

        [JsonProperty("tasks_count")]
        public long TasksCount { get; set; }

        [JsonProperty("team_id")]
        public long TeamId { get; set; }

        [JsonProperty("timesheet_count")]
        public long TimesheetCount { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("write_date")]
        public string WriteDate { get; set; }
    }
}
