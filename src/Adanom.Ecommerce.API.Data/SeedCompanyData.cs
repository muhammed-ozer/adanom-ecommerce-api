using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Data
{
    internal static class SeedCompanyData
    {
        internal static Company Company { get; set; }

        static SeedCompanyData()
        {
            Company = new Company()
            {
                LegalName = "Adanom Mağazacılık Sanayi ve Ticaret Limited Şirketi",
                DisplayName = "Adanom",
                Address = "Topraklık mahallesi Halk Caddesi No: 21/A",
                AddressCityId = 20,
                AddressDistrictId = 361,
                PhoneNumber = "8503462366",
                Email = "info@adanom.com",
                TaxAdministrationId = 0,
                TaxNumber = "0071233691",
                MersisNumber = "0007123369100001"
            };
        }
    }
}
