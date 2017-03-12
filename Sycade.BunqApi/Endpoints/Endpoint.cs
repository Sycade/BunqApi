namespace Sycade.BunqApi.Endpoints
{
    public abstract class Endpoint
    {
        internal BunqApiClient ApiClient { get; }

        internal Endpoint(BunqApiClient apiClient)
        {
            ApiClient = apiClient;
        }
    }
}
