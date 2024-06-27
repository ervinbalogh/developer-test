using Moq;
using NUnit.Framework;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.VatServices.Tests
{
    [TestFixture]
    public class VatServiceFranceTests
    {
        [Test]
        public async Task RegisterVatFrance_Queue_Client_Is_Called()
        {
            var taxuallyQueueClientMock = new Mock<ITaxuallyQueueClient>();
            var vatService = new VatServiceFrance(taxuallyQueueClientMock.Object);
            var request = new VatRegistrationRequestModel
            {
                CompanyId = "1",
                CompanyName = "Company",
                Country = "FR"
            };

            taxuallyQueueClientMock.Setup(x => x.EnqueueAsync(It.IsAny<string>(), It.IsAny<It.IsAnyType>())).Verifiable();

            await vatService.RegisterVat(request);

            taxuallyQueueClientMock.Verify(x => x.EnqueueAsync("vat-registration-xml", It.IsAny<object>()), Times.Once());
        }
    }
}