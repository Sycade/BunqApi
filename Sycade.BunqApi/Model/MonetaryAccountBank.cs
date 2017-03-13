using Sycade.BunqApi.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Model
{
    public class MonetaryAccountBank : MonetaryAccount
    {
        public async Task<Id> CreatePaymentAsync(Alias to, Amount amount, string description, Session session)
        {
            return await ApiClient.Payment.CreateAsync(Id, to, amount, description, session);
        }

        public async Task<Id> CreatePaymentAsync(MonetaryAccountBank to, Amount amount, string description, Session session)
        {
            return await ApiClient.Payment.CreateAsync(Id, to.Aliases.First(a => a.Type == AliasType.IBAN), amount, description, session);
        }
    }
}
