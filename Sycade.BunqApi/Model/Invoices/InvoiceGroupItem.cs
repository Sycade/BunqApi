using Newtonsoft.Json;
using System;

namespace Sycade.BunqApi.Model.Invoices
{
    public class InvoiceGroupItem : BunqEntity
    {
        [JsonProperty("billing_date")]
        public DateTime BillingDate { get; set; }

        [JsonProperty("type_description")]
        public string TypeDescription { get; set; }
        [JsonProperty("type_description_translated")]
        public string TypeDescriptionTranslated { get; set; }

        [JsonProperty("unit_vat_exclusive")]
        public Amount UnitVatExclusive { get; set; }
        [JsonProperty("unit_vat_inclusive")]
        public Amount UnitVatInclusive { get; set; }

        [JsonProperty("vat")]
        public decimal Vat { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("total_vat_exclusive")]
        public Amount TotalVatExclusive { get; set; }
        [JsonProperty("total_vat_inclusive")]
        public Amount TotalVatInclusive { get; set; }
    }
}