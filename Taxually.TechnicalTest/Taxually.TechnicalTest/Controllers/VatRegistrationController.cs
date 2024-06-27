using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.VatServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taxually.TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatRegistrationController : ControllerBase
    {
        private ITaxuallyQueueClient taxuallyQueueClient;
        private ITaxuallyHttpClient taxuallyHttpClient;

        public VatRegistrationController(ITaxuallyQueueClient taxuallyQueueClient, ITaxuallyHttpClient taxuallyHttpClient)
        {
            this.taxuallyQueueClient = taxuallyQueueClient;
            this.taxuallyHttpClient = taxuallyHttpClient;
        }

        /// <summary>
        /// Registers a company for a VAT number in a given country
        /// </summary>
        [HttpPost()]
        public async Task<ActionResult> Post([FromBody] VatRegistrationRequestModel request)
        {
            switch (request.Country)
            {
                case "GB":
                    var vatServiceUK = new VatServiceUK(taxuallyHttpClient);
                    await vatServiceUK.RegisterVat(request);
                    break;
                case "FR":
                    var vatServiceFrance = new VatServiceFrance(taxuallyQueueClient);
                    await vatServiceFrance.RegisterVat(request);
                    break;
                case "DE":
                    var vatServiceGermany = new VatServiceGermany(taxuallyQueueClient);
                    await vatServiceGermany.RegisterVat(request);
                    break;
                default:
                    throw new NotSupportedException("Country not supported");
            }
            return Ok();
        }
    }
}
