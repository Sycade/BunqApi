using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sycade.BunqApi.Exceptions;
using Sycade.BunqApi.Extensions;
using Sycade.BunqApi.Model;
using Sycade.BunqApi.Requests;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sycade.BunqApi
{
    public class BunqApiClient
    {
        private const string BunqApiUrlFormatString = "https://api.bunq.com/v{0}/{1}";
        private const string BunqSandboxApiUrlFormatString = "https://sandbox.public.api.bunq.com/v{0}/{1}";
        private const int ApiVersion = 1;

        private string _apiKey;
        private X509Certificate2 _clientCertificate;
        private string _urlFormatString;
        private string _sessionToken;

        /// <summary>
        /// Your installation token. Will be set automatically when calling CreateInstallationAsync.
        /// </summary>
        public string InstallationToken { get; set; }


        public BunqApiClient(string apiKey, string installationToken, X509Certificate2 clientCertificate, bool useSandbox)
        {
            _apiKey = apiKey;
            _clientCertificate = clientCertificate;
            _urlFormatString = useSandbox ? BunqSandboxApiUrlFormatString : BunqApiUrlFormatString;

            InstallationToken = installationToken;
        }

        public BunqApiClient(string apiKey, X509Certificate2 clientCertificate, bool useSandbox)
            : this(apiKey, null, clientCertificate, useSandbox)
        {
        }


        #region DeviceServer
        public async Task<DeviceServer> CreateDeviceServerAsync(string description, params string[] permittedIpAddresses)
        {
            var request = new CreateDeviceServerRequest(description, _apiKey);

            return await DoSignedApiRequest<DeviceServer>(HttpMethod.Post, "device-server", InstallationToken, request);
        }
        #endregion

        #region Installation
        public async Task<Installation> CreateInstallationAsync()
        {
            var request = new CreateInstallationRequest(_clientCertificate.GetRSAPublicKey().AsPemString());

            var installation = await DoApiRequest<Installation>(HttpMethod.Post, "installation", request);
            InstallationToken = installation.Token.Value;

            return installation;
        }
        #endregion

        #region SessionServer
        public async Task<SessionServer> CreateSessionServerAsync()
        {
            var request = new CreateSessionServerRequest(_apiKey);

            var sessionServer = await DoSignedApiRequest<SessionServer>(HttpMethod.Post, "session-server", InstallationToken, request);
            _sessionToken = sessionServer.Token.Value;

            return sessionServer;
        }
        #endregion


        private async Task<TResponse> DoApiRequest<TResponse>(HttpMethod method, string endpoint, IBunqApiRequest request)
        {
            var httpRequest = CreateRequestMessage(method, endpoint, JsonConvert.SerializeObject(request));

            var responseString = await SendRequestMessageAsync(httpRequest);

            return await ParseResponseAsync<TResponse>(responseString);
        }

        private async Task<TResponse> DoSignedApiRequest<TResponse>(HttpMethod method, string endpoint, string token, IBunqApiRequest request)
        {
            var httpRequest = CreateSignedRequestMessage(method, endpoint, token, JsonConvert.SerializeObject(request));

            var responseString = await SendRequestMessageAsync(httpRequest);

            return await ParseResponseAsync<TResponse>(responseString);
        }

        private async Task<HttpResponseMessage> SendRequestMessageAsync(HttpRequestMessage request)
        {
            using (var httpClient = new HttpClient())
                return await httpClient.SendAsync(request);
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

            request.Content = new StringContent(content);

            return request;
        }

        private HttpRequestMessage CreateSignedRequestMessage(HttpMethod method, string endpoint, string token, string content)
        {
            var request = CreateRequestMessage(method, endpoint, content);

            request.Headers.Add("X-Bunq-Client-Authentication", token);
            request.Headers.Add("X-Bunq-Client-Signature", GetClientSignatureHeader(request, endpoint, content));

            return request;
        }

        private string GetClientSignatureHeader(HttpRequestMessage request, string endpoint, string content)
        {
            var builder = new StringBuilder();

            builder.AppendFormat("{0} /v{1}/{2}\n", request.Method.Method, ApiVersion, endpoint);

            foreach (var header in request.Headers.Where(h => h.Key.StartsWith("X-Bunq-") || h.Key == "Cache-Control" || h.Key == "User-Agent").OrderBy(h => h.Key))
                builder.AppendFormat("{0}: {1}\n", header.Key, header.Value.First());

            builder.Append("\n");
            builder.Append(content);

            var builderBytes = Encoding.UTF8.GetBytes(builder.ToString());
            var signatureData = _clientCertificate.GetRSAPrivateKey().SignData(builderBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            return Convert.ToBase64String(signatureData);
        }


        private async Task<TObject> ParseResponseAsync<TObject>(HttpResponseMessage responseMessage)
        {
            var responseObject = JObject.Parse(await responseMessage.Content.ReadAsStringAsync());
            var responseElements = ((JProperty)responseObject.First).Value as JArray;

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                var error = responseElements.First.ToObject<Error>();

                throw new BunqApiException(error);
            }

            var response = new JObject();

            foreach (var element in responseElements.Cast<JObject>())
            {
                var firstProperty = element.First as JProperty;

                response[firstProperty.Name] = firstProperty.Value;
            }

            return response.ToObject<TObject>();
        }
    }
}
