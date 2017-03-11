using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sycade.BunqApi.Converters;
using Sycade.BunqApi.Exceptions;
using Sycade.BunqApi.Extensions;
using Sycade.BunqApi.Model;
using Sycade.BunqApi.Requests;
using Sycade.BunqApi.Responses;
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
    public class BunqApiClient
    {
        private const string BunqApiUrlFormatString = "https://api.bunq.com/v{0}/{1}";
        private const string BunqSandboxApiUrlFormatString = "https://sandbox.public.api.bunq.com/v{0}/{1}";
        private const int ApiVersion = 1;

        private string _apiKey;
        private X509Certificate2 _clientCertificate;
        private string _urlFormatString;

        public BunqApiClient(string apiKey, X509Certificate2 clientCertificate, bool useSandbox)
        {
            _apiKey = apiKey;
            _clientCertificate = clientCertificate;
            _urlFormatString = useSandbox ? BunqSandboxApiUrlFormatString : BunqApiUrlFormatString;
        }


        #region Device Server
        public async Task<Id> CreateDeviceServerAsync(string description, Token installationToken)
        {
            var request = new CreateDeviceServerRequest(description, _apiKey);

            var responseObjects = await DoSignedApiRequest(HttpMethod.Post, "device-server", installationToken, request);

            return responseObjects.Cast<Id>().First();
        }
        #endregion

        #region Installation
        public async Task<CreateInstallationResponse> CreateInstallationAsync()
        {
            var request = new CreateInstallationRequest(_clientCertificate.GetRSAPublicKey().AsPemString());

            var responseObjects = await DoApiRequest(HttpMethod.Post, "installation", request);
            var response = new CreateInstallationResponse(responseObjects);

            //InstallationToken = response.Token.Value;

            return response;
        }
        #endregion

        #region Monetary Account
        public async Task<MonetaryAccountBank> GetMonetaryAccountBankAsync(User user, int monetaryAccountId, Token sessionToken)
        {
            var responseObjects = await DoSignedApiRequest(HttpMethod.Get, $"user/{user.Id}/monetary-account-bank/{monetaryAccountId}", sessionToken);

            return responseObjects.Cast<MonetaryAccountBank>().First();
        }

        public async Task<MonetaryAccountBank[]> ListMonetaryAccountBanksAsync(User user, Token sessionToken)
        {
            var responseObjects = await DoSignedApiRequest(HttpMethod.Get, $"user/{user.Id}/monetary-account-bank", sessionToken);

            return responseObjects.Cast<MonetaryAccountBank>().ToArray();
        }
        #endregion

        #region Payment
        public async Task<Id> CreatePaymentAsync(User user, int fromAccountId, Alias toAccount, Amount amount, string description, Token sessionToken)
        {
            var request = new CreatePaymentRequest(amount, toAccount, description);

            var responseObjects = await DoSignedApiRequest(HttpMethod.Post, $"user/{user.Id}/monetary-account/{fromAccountId}/payment", sessionToken, request);

            return responseObjects.Cast<Id>().First();
        }
        #endregion

        #region Session Server
        public async Task<CreateSessionServerResponse> CreateSessionServerAsync(Token installationToken)
        {
            var request = new CreateSessionServerRequest(_apiKey);

            var responseObjects = await DoSignedApiRequest(HttpMethod.Post, "session-server", installationToken, request);
            var response = new CreateSessionServerResponse(responseObjects);

            return response;
        }
        #endregion


        private async Task<IBunqEntity[]> DoApiRequest(HttpMethod method, string endpoint, IBunqApiRequest request = null)
        {
            var content = request != null ? JsonConvert.SerializeObject(request) : "";

            var httpRequest = CreateRequestMessage(method, endpoint, content);
            var httpResponse = await SendRequestMessageAsync(httpRequest);

            return await GetResponseObjectsAsync(httpResponse);
        }

        private async Task<IBunqEntity[]> DoSignedApiRequest(HttpMethod method, string endpoint, Token token, IBunqApiRequest request = null)
        {
            var content = request != null ? JsonConvert.SerializeObject(request) : "";

            var httpRequest = CreateSignedRequestMessage(method, endpoint, token, content);
            var httpResponse = await SendRequestMessageAsync(httpRequest);

            return await GetResponseObjectsAsync(httpResponse);
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


        private async Task<IBunqEntity[]> GetResponseObjectsAsync(HttpResponseMessage responseMessage)
        {
            var responseObject = JObject.Parse(await responseMessage.Content.ReadAsStringAsync());
            var responseArray = (JArray)((JProperty)responseObject.First).Value;

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                var error = responseArray.First.ToObject<Error>();

                throw new BunqApiException(error);
            }

            var responseObjects = new List<IBunqEntity>();

            foreach (var element in responseArray.Cast<JObject>())
            {
                var property = (JProperty)element.First;
                var propertyValue = (JObject)property.Value;

                var type = ModelFinder.FindByName(property.Name);

                responseObjects.Add((IBunqEntity)propertyValue.ToObject(type));
            }

            return responseObjects.ToArray();
        }

        private JsonSerializerSettings GetJsonSerializerSettings()
        {
            var settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                {
                    new CurrencyConverter()
                }
            };

            return settings;
        }
    }
}
