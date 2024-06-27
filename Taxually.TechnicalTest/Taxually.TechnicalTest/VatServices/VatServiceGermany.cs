using System.Text;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.VatServices
{
    public class VatServiceGermany: IVatService
    {
        private ITaxuallyQueueClient taxuallyQueueClient;

        public VatServiceGermany(ITaxuallyQueueClient taxuallyQueueClient)
        {
            this.taxuallyQueueClient = taxuallyQueueClient;
        }
        public async Task RegisterVat(VatRegistrationRequestModel request)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("CompanyName,CompanyId");
            csvBuilder.AppendLine($"{request.CompanyName},{request.CompanyId}");
            var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());

            await taxuallyQueueClient.EnqueueAsync("vat-registration-csv", csv);
        }
    }
}
