namespace Sycade.BunqApi.Model
{
    internal interface IBunqInteractableEntity : IBunqEntity
    {
        void Initialize(BunqHttpClient apiClient);
    }
}
