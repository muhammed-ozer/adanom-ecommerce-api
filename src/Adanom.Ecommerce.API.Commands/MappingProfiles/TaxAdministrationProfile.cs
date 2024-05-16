namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class TaxAdministrationProfile : Profile
    {
        public TaxAdministrationProfile()
        {
            CreateMap<TaxAdministration, TaxAdministrationResponse>();

            CreateMap<TaxAdministrationResponse, TaxAdministration>();

            CreateMap<CreateTaxAdministrationRequest, CreateTaxAdministration>();

            CreateMap<CreateTaxAdministration, TaxAdministration>();

            CreateMap<UpdateTaxAdministrationRequest, UpdateTaxAdministration>();

            CreateMap<UpdateTaxAdministration, TaxAdministration>();

            CreateMap<DeleteTaxAdministrationRequest, DeleteTaxAdministration>();
        }
    }
}