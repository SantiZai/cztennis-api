using Microsoft.AspNetCore.Mvc;
using MercadoPago.Config;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using api.Entities;
using api.Services;

namespace api.Payments
{
    [ApiController]
    [Route("create-preference")]
    public class PaymentsControllers : ControllerBase
    {
        private readonly StrungsServices _strungsService;

        public PaymentsControllers(StrungsServices strungsService)
        {
            _strungsService = strungsService;
        }

        [HttpPost]
        public async Task<Preference> CreatePreference(List<Order> orders)
        {
            List<Strung> strs = new(_strungsService.GetAllByIds(orders));

            MercadoPagoConfig.AccessToken = "TEST-7204989668375833-073020-a25f6ee3cea08b9574c770da92ad2b95-1436768984";
            var request = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>(),
                Purpose = "wallet_purchase",
            };
            foreach (Strung str in strs)
            {
                request.Items.Add(new PreferenceItemRequest
                {
                    Title = str.Name,
                    Quantity = 1,
                    CurrencyId = "ARS",
                    UnitPrice = (decimal)str.Price!,
                    Description = $"{str.Brand} {str.Name} {str.Size}",
                });
            }

            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);
            return preference;
        }
    }
}