using Sycade.BunqApi.Endpoints;

namespace Sycade.BunqApi
{
    public partial class BunqApiClient
    {
        public CardEndpoint Cards { get; private set; }
        public CustomerStatementEndpoint CustomerStatements { get; private set; }
        public DeviceEndpoint Devices { get; private set; }
        public DeviceServerEndpoint DeviceServers { get; private set; }
        public InstallationEndpoint Installations { get; private set; }
        public InvoiceEndpoint Invoices { get; private set; }
        public MonetaryAccountBankEndpoint MonetaryAccountBanks { get; private set; }
        public PaymentEndpoint Payments { get; private set; }
        public SessionEndpoint Sessions { get; private set; }

        private void InitializeEndpoints()
        {
            Cards = new CardEndpoint(this);
            CustomerStatements = new CustomerStatementEndpoint(this);
            Devices = new DeviceEndpoint(this);
            DeviceServers = new DeviceServerEndpoint(this);
            Installations = new InstallationEndpoint(this);
            Invoices = new InvoiceEndpoint(this);
            MonetaryAccountBanks = new MonetaryAccountBankEndpoint(this);
            Payments = new PaymentEndpoint(this);
            Sessions = new SessionEndpoint(this);
        }
    }
}
