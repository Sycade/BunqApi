using Sycade.BunqApi.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Model
{
    public class MonetaryAccountBank : MonetaryAccount, IBunqInteractableEntity
    {
        private BunqApiClient _apiClient;

        void IBunqInteractableEntity.Initialize(BunqApiClient apiClient)
        {
            _apiClient = apiClient;
        }


        public async Task<Id> CreatePaymentAsync(Amount amount, Alias to, string description, Session session)
        {
            return await _apiClient.Payment.CreateAsync(Id, to, amount, description, session);
        }

        public async Task<Id> CreatePaymentAsync(Amount amount, MonetaryAccountBank to, string description, Session session)
        {
            return await _apiClient.Payment.CreateAsync(Id, to.Aliases.First(a => a.Type == AliasType.IBAN), amount, description, session);
        }
    }
}
