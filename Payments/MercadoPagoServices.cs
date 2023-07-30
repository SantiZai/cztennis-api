using api.Entities;
using MercadoPago.Client;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;

namespace api.Payments
{
    public class MercadoPagoServices
    {
        public async Task<Preference> ProcessPayment()
        {
            const string acc = "TEST-3469506575384192-072914-71c3475f0690104d86418f34e97c6354-1436768984";

            RequestOptions reqOptions = new()
            {
                AccessToken = acc,
            };

            PreferenceRequest request = new()
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = "Max Power",
                        Quantity = 1,
                        CurrencyId = "ARS",
                        UnitPrice = 6500,
                    }
                }
            };
            PreferenceClient client = new();
            Preference preference = await client.CreateAsync(request, reqOptions);
            return preference;
        }
    }
}
