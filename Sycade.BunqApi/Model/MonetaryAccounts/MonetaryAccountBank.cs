using System.Linq;
using System.Threading.Tasks;

namespace Sycade.BunqApi.Model.MonetaryAccounts
{
    public class MonetaryAccountBank : MonetaryAccount
    {
        public async Task<Id> CreatePaymentAsync(Alias to, Amount amount, string description)
        {
            return await ApiClient.Payments.CreateAsync(Id, to, amount, description);
        }

        public async Task<Id> CreatePaymentAsync(MonetaryAccountBank to, Amount amount, string description)
        {
            return await ApiClient.Payments.CreateAsync(Id, to.Aliases.First(a => a.Type == AliasType.IBAN), amount, description);
        }
    }
}
