using Sycade.BunqApi.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Model
{
    public class MonetaryAccountBank : MonetaryAccount, IBunqInteractableEntity
    {
        public BunqApiClient ApiClient { get; set; }


        public async Task<Id> CreatePaymentAsync(Amount amount, Alias to, string description, Session session)
        {
            return await ApiClient.Payment.CreateAsync(Id, to, amount, description, session);
        }

        public async Task<Id> CreatePaymentAsync(Amount amount, MonetaryAccountBank to, string description, Session session)
        {
            return await ApiClient.Payment.CreateAsync(Id, to.Aliases.First(a => a.Type == AliasType.IBAN), amount, description, session);
        }
    }
}
