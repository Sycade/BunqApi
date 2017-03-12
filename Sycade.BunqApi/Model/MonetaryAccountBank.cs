using Sycade.BunqApi.Endpoints;
using Sycade.BunqApi.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Model
{
    public class MonetaryAccountBank : MonetaryAccount, IBunqInteractableEntity
    {
        private PaymentEndpoint _paymentEndpoint;

        void IBunqInteractableEntity.Initialize(BunqHttpClient apiClient)
        {
            _paymentEndpoint = new PaymentEndpoint(apiClient);
        }


        public async Task<Id> CreatePaymentAsync(Amount amount, Alias to, string description, Session session)
        {
            return await _paymentEndpoint.CreateAsync(Id, to, amount, description, session);
        }

        public async Task<Id> CreatePaymentAsync(Amount amount, MonetaryAccountBank to, string description, Session session)
        {
            return await _paymentEndpoint.CreateAsync(Id, to.Aliases.First(a => a.Type == AliasType.IBAN), amount, description, session);
        }
    }
}
