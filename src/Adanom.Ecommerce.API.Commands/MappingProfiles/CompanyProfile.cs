namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyResponse>();

            CreateMap<CompanyResponse, Company>();

            CreateMap<UpdateCompanyRequest, UpdateCompany>();

            CreateMap<UpdateCompany, Company>();
        }
    }
}