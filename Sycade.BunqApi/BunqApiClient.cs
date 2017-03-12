using Sycade.BunqApi.Endpoints;
using Sycade.BunqApi.Model;
using System.Security.Cryptography.X509Certificates;

namespace Sycade.BunqApi
{
    public class BunqApiClient : BunqHttpClient
    {
        public DeviceServerEndpoint DeviceServer { get; private set; }
        public InstallationEndpoint Installation { get; private set; }
        public MonetaryAccountBankEndpoint MonetaryAccountBank { get; private set; }
        public SessionEndpoint Session { get; private set; }
        public SessionServerEndpoint SessionServer { get; private set; }

        public BunqApiClient(string apiKey, X509Certificate2 clientCertificate, ServerPublicKey serverPublicKey, bool useSandbox)
            : base(apiKey, clientCertificate, serverPublicKey, useSandbox)
        {
            InitializeEndpoints();
        }

        public BunqApiClient(string apiKey, X509Certificate2 clientCertificate, bool useSandbox)
            : this(apiKey, clientCertificate, null, useSandbox) { }


        private void InitializeEndpoints()
        {
            DeviceServer = new DeviceServerEndpoint(this);
            Installation = new InstallationEndpoint(this);
            MonetaryAccountBank = new MonetaryAccountBankEndpoint(this);
            Session = new SessionEndpoint(this);
            SessionServer = new SessionServerEndpoint(this);
        }
    }
}
