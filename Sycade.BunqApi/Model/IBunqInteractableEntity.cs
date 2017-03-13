namespace Sycade.BunqApi.Model
{
    internal interface IBunqInteractableEntity : IBunqEntity
    {
        BunqApiClient ApiClient { get; set; }
    }
}
