using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Helpers;

namespace Taxually.TechnicalTest.VatServices
{
    public class VatServiceFrance: IVatService
    {
        private ITaxuallyQueueClient taxuallyQueueClient;

        public VatServiceFrance(ITaxuallyQueueClient taxuallyQueueClient)
        {
            this.taxuallyQueueClient = taxuallyQueueClient;
        }

        public async Task RegisterVat(VatRegistrationRequestModel request)
        {
            var xml = Serializer.SerializeToXml(request);

            await taxuallyQueueClient.EnqueueAsync("vat-registration-xml", xml);
        }
    }
}
