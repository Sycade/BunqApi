using Newtonsoft.Json;
using Sycade.BunqApi.Converters;
using Sycade.BunqApi.Model.CustomerStatements;
using System;

namespace Sycade.BunqApi.Requests
{
    public class CreateCustomerStatementRequest : IBunqApiRequest
    {
        [JsonProperty("statement_format")]
        [JsonConverter(typeof(EnumToStringConverter))]
        public CustomerStatementFormat Format { get; set; }

        [JsonProperty("regional_format")]
        [JsonConverter(typeof(EnumToStringConverter))]
        public CustomerStatementRegionalFormat RegionalFormat { get; set; }

        [JsonProperty("date_start")]
        public DateTime DateStart { get; set; }
        [JsonProperty("date_end")]
        public DateTime DateEnd { get; set; }

        public CreateCustomerStatementRequest(DateTime startDate, DateTime endDate, CustomerStatementFormat format, CustomerStatementRegionalFormat regionalFormat)
        {
            DateStart = startDate;
            DateEnd = endDate;
            Format = format;
            RegionalFormat = regionalFormat;
        }

        public CreateCustomerStatementRequest(DateTime startDate, DateTime endDate, CustomerStatementFormat format)
            : this(startDate, endDate, format, CustomerStatementRegionalFormat.EUROPEAN) { }
    }
}
