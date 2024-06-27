using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Taxually.TechnicalTest;
using Taxually.TechnicalTest.Controllers;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTestTests
{
    [TestFixture]
    public class VatRegistrationControllerTests
    {
        [Test]
        public async Task Post_UK_Company_Registration_Is_Successful()
        {
            var taxuallyQueueClientMock = new Mock<ITaxuallyQueueClient>();
            var httpClientMock = new Mock<ITaxuallyHttpClient>();

            var controller = new VatRegistrationController(taxuallyQueueClientMock.Object, httpClientMock.Object);

            var request = new VatRegistrationRequestModel
            {
                CompanyId = "1",
                CompanyName = "Company",
                Country = "GB"
            };

            httpClientMock.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<VatRegistrationRequestModel>())).Verifiable();

            var response = await controller.Post(request);

            httpClientMock.Verify(x => x.PostAsync("https://api.uktax.gov.uk", It.IsAny<VatRegistrationRequestModel>()), Times.Once());
        }

        [Test]
        public void Post_PL_Company_Throws_Unsupported_Exception()
        {
            var taxuallyQueueClientMock = new Mock<ITaxuallyQueueClient>();
            var httpClientMock = new Mock<ITaxuallyHttpClient>();

            var controller = new VatRegistrationController(taxuallyQueueClientMock.Object, httpClientMock.Object);

            var request = new VatRegistrationRequestModel
            {
                CompanyId = "1",
                CompanyName = "Company",
                Country = "PL"
            };

            var ex =NUnit.Framework.Assert.ThrowsAsync<NotSupportedException>(async () => await controller.Post(request));

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Country not supported", ex.Message);
        }
    }
}
