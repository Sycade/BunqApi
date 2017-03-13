using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sycade.BunqApi.Endpoints;
using Sycade.BunqApi.Exceptions;
using Sycade.BunqApi.Extensions;
using Sycade.BunqApi.Model;
using Sycade.BunqApi.Requests;
using Sycade.BunqApi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sycade.BunqApi
{
    /// <summary>
    /// Executes HTTP requests to the bunq API and parses responses.
    /// </summary>
    public class BunqApiClient
    {
        private const string BunqApiUrlFormatString = "https://api.bunq.com/v{0}/{1}";
        private const string BunqSandboxApiUrlFormatString = "https://sandbox.public.api.bunq.com/v{0}/{1}";
        private const int ApiVersion = 1;

        private const string ClientSignatureHeaderName = "X-Bunq-Client-Signature";
        private const string ServerSignatureHeaderName = "X-Bunq-Server-Signature";

        private RSA _serverPublicKey;
        private string _urlFormatString;

        internal string ApiKey { get; }
        internal X509Certificate2 ClientCertificate { get; }

        public DeviceServerEndpoint DeviceServer { get; private set; }
        public InstallationEndpoint Installation { get; private set; }
        public MonetaryAccountBankEndpoint MonetaryAccountBank { get; private set; }
        public PaymentEndpoint Payment { get; private set; }
        public SessionEndpoint Session { get; private set; }
        public SessionServerEndpoint SessionServer { get; private set; }

        public BunqApiClient(string apiKey, X509Certificate2 clientCertificate, ServerPublicKey serverPublicKey, bool useSandbox)
        {
            ApiKey = apiKey;
            ClientCertificate = clientCertificate;

            _urlFormatString = useSandbox ? BunqSandboxApiUrlFormatString : BunqApiUrlFormatString;

            if (serverPublicKey != null)
                SetServerPublicKey(serverPublicKey);

            InitializeEndpoints();
        }

        public BunqApiClient(string apiKey, X509Certificate2 clientCertificate, bool useSandbox)
            : this(apiKey, clientCertificate, null, useSandbox) { }


        public void SetServerPublicKey(ServerPublicKey serverPublicKey)
        {
            _serverPublicKey = RSAExtensions.FromPemString(serverPublicKey.Value);
        }

        private void InitializeEndpoints()
        {
            DeviceServer = new DeviceServerEndpoint(this);
            Installation = new InstallationEndpoint(this);
            MonetaryAccountBank = new MonetaryAccountBankEndpoint(this);
            Payment = new PaymentEndpoint(this);
            Session = new SessionEndpoint(this);
            SessionServer = new SessionServerEndpoint(this);
        }


        internal async Task<IBunqEntity[]> DoApiRequestAsync(HttpMethod method, string endpoint, IBunqApiRequest request = null)
        {
            var requestContent = request != null ? JsonConvert.SerializeObject(request) : "";
            var requestMessage = CreateRequestMessage(method, endpoint, requestContent);

            var responseMessage = await SendRequestMessageAsync(requestMessage);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var responseArray = ParseResponse(responseMessage, responseContent);

            return GetEntities(responseArray);
        }

        internal async Task<IBunqEntity[]> DoSignedApiRequestAsync(HttpMethod method, string endpoint, Token token, IBunqApiRequest request = null)
        {
            if (_serverPublicKey == null)
                throw new BunqApiException("Server public key was not set.");

            var requestContent = request != null ? JsonConvert.SerializeObject(request) : "";
            var requestMessage = CreateSignedRequestMessage(method, endpoint, token, requestContent);

            var responseMessage = await SendRequestMessageAsync(requestMessage);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var responseArray = ParseResponse(responseMessage, responseContent);

            VerifyResponse(responseMessage, responseContent);

            return GetEntities(responseArray);
        }

        private async Task<HttpResponseMessage> SendRequestMessageAsync(HttpRequestMessage requestMessage)
        {
            using (var httpClient = new HttpClient())
                return await httpClient.SendAsync(requestMessage);
        }


        private HttpRequestMessage CreateRequestMessage(HttpMethod method, string endpoint, string content)
        {
            var request = new HttpRequestMessage(method, string.Format(_urlFormatString, ApiVersion, endpoint));

            request.Headers.Add("Cache-Control", "no-cache");
            request.Headers.Add("User-Agent", "sycade.bunq/0.0.1");
            request.Headers.Add("X-Bunq-Client-Request-Id", Guid.NewGuid().ToString("N"));
            request.Headers.Add("X-Bunq-Geolocation", "0 0 0 0 NL");
            request.Headers.Add("X-Bunq-Language", "nl_NL");
            request.Headers.Add("X-Bunq-Region", "nl_NL");

            if (!string.IsNullOrWhiteSpace(content))
                request.Content = new StringContent(content);

            return request;
        }

        private HttpRequestMessage CreateSignedRequestMessage(HttpMethod method, string endpoint, Token token, string content)
        {
            var request = CreateRequestMessage(method, endpoint, content);

            request.Headers.Add("X-Bunq-Client-Authentication", token.Value);

            SignRequest(request, endpoint, content);

            return request;
        }

        private void SignRequest(HttpRequestMessage requestMessage, string endpoint, string content)
        {
            var builder = new StringBuilder();

            builder.AppendFormat("{0} /v{1}/{2}\n", requestMessage.Method.Method, ApiVersion, endpoint);

            foreach (var header in requestMessage.Headers.Where(h => h.Key.StartsWith("X-Bunq-") || h.Key == "Cache-Control" || h.Key == "User-Agent").OrderBy(h => h.Key))
                builder.AppendFormat("{0}: {1}\n", header.Key, header.Value.First());

            builder.Append("\n");
            builder.Append(content);

            var builderBytes = Encoding.UTF8.GetBytes(builder.ToString());
            var clientSignature = ClientCertificate.GetRSAPrivateKey().SignData(builderBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            requestMessage.Headers.Add(ClientSignatureHeaderName, Convert.ToBase64String(clientSignature));
        }


        private JArray ParseResponse(HttpResponseMessage responseMessage, string responseContent)
        {
            var parsedObject = JObject.Parse(responseContent);
            var responseArray = (JArray)((JProperty)parsedObject.First).Value;

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                var error = responseArray.First.ToObject<Error>();
                throw new BunqApiException(error);
            }

            return responseArray;
        }

        private void VerifyResponse(HttpResponseMessage responseMessage, string content)
        {
            var serverSignatureHeader = responseMessage.Headers.FirstOrDefault(h => h.Key == ServerSignatureHeaderName).Value.FirstOrDefault();

            if (serverSignatureHeader == null)
                throw new BunqApiException("Server sent an invalid response. No signature header found.");

            var builder = new StringBuilder();

            builder.AppendFormat("{0}\n", (int)responseMessage.StatusCode);

            foreach (var header in responseMessage.Headers.Where(h => h.Key.StartsWith("X-Bunq-") && h.Key != ServerSignatureHeaderName).OrderBy(h => h.Key))
                builder.AppendFormat("{0}: {1}\n", header.Key, header.Value.First());

            builder.Append("\n");
            builder.Append(content);

            var builderBytes = Encoding.UTF8.GetBytes(builder.ToString());
            var serverSignature = Convert.FromBase64String(serverSignatureHeader);

            if (!_serverPublicKey.VerifyData(builderBytes, serverSignature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1))
                throw new BunqApiException("Server sent an invalid response. Could not verify signature.");
        }


        private IBunqEntity[] GetEntities(JArray responseArray)
        { 
            var entities = new List<IBunqEntity>();

            foreach (var element in responseArray.Cast<JObject>())
            {
                var property = (JProperty)element.First;
                var propertyValue = (JObject)property.Value;

                var type = EntityTypeCollection.FindByName(property.Name);
                var entity = (IBunqEntity)propertyValue.ToObject(type);

                if (entity is IBunqInteractableEntity interactable)
                    interactable.ApiClient = this;

                entities.Add(entity);
            }

            return entities.ToArray();
        }
    }
}
