using api.Entities;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using Microsoft.AspNetCore.Mvc;

namespace api.Payments
{
    [ApiController]
    [Route("checkout/preferences")]
    public class MercadoPagoControllers : ControllerBase
    {
        private readonly MercadoPagoServices _services;
        
        public MercadoPagoControllers(MercadoPagoServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<string> GeneratePreference(Strung strung)
        {
            MercadoPagoConfig.AccessToken = "TEST-3469506575384192-072914-71c3475f0690104d86418f34e97c6354-1436768984";
            PreferenceClient client = new();
            PreferenceRequest request = new();
            request.Items.Add(new PreferenceItemRequest
            {
                Id = strung.Id.ToString(),
                Title = strung.Name,
                Quantity = 1,
                CurrencyId = "ARS",
                UnitPrice = (decimal)strung.Price!,
            });
            request.BackUrls = new PreferenceBackUrlsRequest()
            {
                Failure = "Fallo",
                Success = "Exitoso",
                Pending = "Pendiente",
            };
            request.NotificationUrl = "webhook";
            request.Payer = new PreferencePayerRequest()
            {
                Name = "Nombre",
                Email = "test@test.com"
            };
            var res = await client.CreateAsync(request);
            return res.InitPoint;
        }
    }
}
