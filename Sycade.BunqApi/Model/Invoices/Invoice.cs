using Newtonsoft.Json;
using System;

namespace Sycade.BunqApi.Model.Invoices
{
    public class Invoice : BunqEntity
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        [JsonProperty("invoice_date")]
        public DateTime InvoiceDate { get; set; }
        [JsonProperty("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("group")]
        public InvoiceGroup[] Groups { get; set; }

        [JsonProperty("total_vat_exclusive")]
        public Amount TotalVatExclusive { get; set; }
        [JsonProperty("total_vat_inclusive")]
        public Amount TotalVatInclusive { get; set; }
        [JsonProperty("total_vat")]
        public Amount TotalVat { get; set; }

        [JsonProperty("alias")]
        public InvoiceAlias Alias { get; set; }
        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("counterparty_alias")]
        public InvoiceAlias CounterpartyAlias { get; set; }
        [JsonProperty("counterparty_address")]
        public Address CounterpartyAddress { get; set; }

        [JsonProperty("chamber_of_commerce_number")]
        public string ChamberOfCommerceNumber { get; set; }
        [JsonProperty("vat_number")]
        public string VatNumber { get; set; }
    }
}
