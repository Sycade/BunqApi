namespace Sycade.BunqApi.Endpoints
{
    public abstract class Endpoint
    {
        internal BunqApiClient ApiClient { get; }

        public Endpoint(BunqApiClient apiClient)
        {
            ApiClient = apiClient;
        }
    }
}
