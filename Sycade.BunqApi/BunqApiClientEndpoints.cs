using Sycade.BunqApi.Endpoints;

namespace Sycade.BunqApi
{
    public partial class BunqApiClient
    {
        public CardEndpoint Card { get; private set; }
        public CustomerStatementEndpoint CustomerStatement { get; private set; }
        public DeviceEndpoint Device { get; private set; }
        public DeviceServerEndpoint DeviceServer { get; private set; }
        public InstallationEndpoint Installation { get; private set; }
        public MonetaryAccountBankEndpoint MonetaryAccountBank { get; private set; }
        public PaymentEndpoint Payment { get; private set; }
        public SessionEndpoint Session { get; private set; }
        public SessionServerEndpoint SessionServer { get; private set; }

        private void InitializeEndpoints()
        {
            Card = new CardEndpoint(this);
            CustomerStatement = new CustomerStatementEndpoint(this);
            Device = new DeviceEndpoint(this);
            DeviceServer = new DeviceServerEndpoint(this);
            Installation = new InstallationEndpoint(this);
            MonetaryAccountBank = new MonetaryAccountBankEndpoint(this);
            Payment = new PaymentEndpoint(this);
            Session = new SessionEndpoint(this);
            SessionServer = new SessionServerEndpoint(this);
        }
    }
}
