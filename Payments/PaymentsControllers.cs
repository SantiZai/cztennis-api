using Microsoft.AspNetCore.Mvc;
using MercadoPago.Config;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using api.Entities;

namespace api.Payments
{
    [ApiController]
    [Route("create-preference")]
    public class PaymentsControllers : ControllerBase
    {
        [HttpPost]
        public async Task<Preference> CreatePreference(Order order)
        {
            MercadoPagoConfig.AccessToken = "TEST-7204989668375833-073020-a25f6ee3cea08b9574c770da92ad2b95-1436768984";
            var request = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = "Prod 1",
                        Quantity = 1,
                        CurrencyId = "ARS",
                        UnitPrice = 500,
                    }
                }
            };
            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);
            return preference;
        }
    }
}
