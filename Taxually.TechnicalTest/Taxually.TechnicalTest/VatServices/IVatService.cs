using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.VatServices
{
    public interface IVatService
    {
        Task RegisterVat(VatRegistrationRequestModel request);
    }
}
