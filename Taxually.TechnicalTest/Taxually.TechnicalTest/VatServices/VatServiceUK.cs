using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.VatServices
{
    public class VatServiceUK: IVatService
    {
        private ITaxuallyHttpClient taxuallyHttpClient;

        public VatServiceUK(ITaxuallyHttpClient taxuallyHttpClient)
        {
            this.taxuallyHttpClient = taxuallyHttpClient;
        }

        public async Task RegisterVat(VatRegistrationRequestModel request)
        {
            await taxuallyHttpClient.PostAsync("https://api.uktax.gov.uk", request);
        }
    }
}
