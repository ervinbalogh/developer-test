using Moq;
using NUnit.Framework;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.VatServices.Tests
{
    [TestFixture]
    public class VatServiceGermanyTests
    {
        [Test]
        public async Task RegisterVatGermany_Queue_Client_Is_Called()
        {
            var taxuallyQueueClientMock = new Mock<ITaxuallyQueueClient>();
            var vatService = new VatServiceGermany(taxuallyQueueClientMock.Object);
            var request = new VatRegistrationRequestModel
            {
                CompanyId = "1",
                CompanyName = "Company",
                Country = "DE"
            };

            taxuallyQueueClientMock.Setup(x => x.EnqueueAsync(It.IsAny<string>(), It.IsAny<It.IsAnyType>())).Verifiable();

            await vatService.RegisterVat(request);

            taxuallyQueueClientMock.Verify(x => x.EnqueueAsync("vat-registration-csv", It.IsAny<object>()), Times.Once());
        }
    }
}