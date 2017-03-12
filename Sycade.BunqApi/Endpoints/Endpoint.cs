namespace Sycade.BunqApi.Endpoints
{
    public abstract class Endpoint
    {
        internal BunqHttpClient ApiClient { get; }

        internal Endpoint(BunqHttpClient apiClient)
        {
            ApiClient = apiClient;
        }
    }
}
