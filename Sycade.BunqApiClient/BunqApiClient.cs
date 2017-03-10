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
        private const int BunqApiVersion = 1;

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
            var response = await DoSignedJsonRequest(HttpMethod.Post, "device-server", InstallationToken, request);

            return response.ToObject<DeviceServer>();
        }
        #endregion

        #region Installation
        public async Task<Installation> CreateInstallationAsync()
        {
            var request = new CreateInstallationRequest(_clientCertificate.GetRSAPublicKey().AsPemString());
            var response = await DoJsonRequest(HttpMethod.Post, "installation", request);

            var installation = response.ToObject<Installation>();
            InstallationToken = installation.Token.Value;

            return installation;
        }
        #endregion

        #region SessionServer
        public async Task<SessionServer> CreateSessionServerAsync()
        {
            var request = new CreateSessionServerRequest(_apiKey);
            var response = await DoSignedJsonRequest(HttpMethod.Post, "session-server", InstallationToken, request);

            var sessionServer = response.ToObject<SessionServer>();
            _sessionToken = sessionServer.Token.Value;

            return sessionServer;
        }
        #endregion


        private async Task<JObject> DoJsonRequest<TRequest>(HttpMethod method, string endpoint, TRequest request)
        {
            var httpRequest = CreateRequestMessage(method, endpoint, JsonConvert.SerializeObject(request));

            var responseString = await DoHttpRequest(httpRequest);

            return await FlattenResponseObjectAsync(responseString);
        }

        private async Task<JObject> DoSignedJsonRequest<TRequest>(HttpMethod method, string endpoint, string token, TRequest request)
        {
            var httpRequest = CreateSignedRequestMessage(method, endpoint, token, JsonConvert.SerializeObject(request));

            var responseString = await DoHttpRequest(httpRequest);

            return await FlattenResponseObjectAsync(responseString);
        }

        private async Task<HttpResponseMessage> DoHttpRequest(HttpRequestMessage request)
        {
            using (var httpClient = new HttpClient())
                return await httpClient.SendAsync(request);
        }


        private HttpRequestMessage CreateRequestMessage(HttpMethod method, string endpoint, string content)
        {
            var request = new HttpRequestMessage(method, string.Format(_urlFormatString, BunqApiVersion, endpoint));

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
            SignRequest(request, endpoint, content);

            return request;
        }

        private void SignRequest(HttpRequestMessage request, string endpoint, string content)
        {
            var builder = new StringBuilder();

            builder.AppendFormat("{0} /v{1}/{2}\n", request.Method.Method, BunqApiVersion, endpoint);

            foreach (var header in request.Headers.Where(h => h.Key.StartsWith("X-Bunq-") || h.Key == "Cache-Control" || h.Key == "User-Agent").OrderBy(h => h.Key))
                builder.AppendFormat("{0}: {1}\n", header.Key, header.Value.First());

            builder.Append("\n");
            builder.Append(content);

            var signatureData = _clientCertificate.GetRSAPrivateKey().SignData(Encoding.UTF8.GetBytes(builder.ToString()), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            request.Headers.Add("X-Bunq-Client-Signature", Convert.ToBase64String(signatureData));
        }

        private async Task<JObject> FlattenResponseObjectAsync(HttpResponseMessage responseMessage)
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

            return response;
        }
    }
}
