using Moq;
using NUnit.Framework;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.VatServices.Tests
{
    [TestFixture]
    public class VatServiceUKTests
    {
        [Test]
        public async Task RegisterVatUK_Http_Client_Is_Called()
        {
            var httpClientMock = new Mock<ITaxuallyHttpClient>();
            var vatService = new VatServiceUK(httpClientMock.Object);
            var request = new VatRegistrationRequestModel
            {
                CompanyId = "1",
                CompanyName = "Company",
                Country = "UK"
            };

            httpClientMock.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<VatRegistrationRequestModel>())).Verifiable();

            await vatService.RegisterVat(request);

            httpClientMock.Verify(x => x.PostAsync("https://api.uktax.gov.uk", It.IsAny<VatRegistrationRequestModel>()), Times.Once());
        }
    }
}