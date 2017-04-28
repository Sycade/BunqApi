using Newtonsoft.Json;

namespace Sycade.BunqApi.Model.Invoices
{
    public class InvoiceGroup : BunqEntity
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("type_description")]
        public string TypeDescription { get; set; }
        [JsonProperty("type_description_translated")]
        public string TypeDescriptionTranslated { get; set; }

        [JsonProperty("instance_description")]
        public string InstanceDescription { get; set; }

        [JsonProperty("product_vat_exclusive")]
        public Amount ProductVatExclusive { get; set; }
        [JsonProperty("product_vat_inclusive")]
        public Amount ProductVatInclusive { get; set; }

        [JsonProperty("item")]
        public InvoiceGroupItem Item { get; set; }
    }
}
