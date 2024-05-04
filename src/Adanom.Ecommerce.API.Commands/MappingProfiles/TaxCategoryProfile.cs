namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class TaxCategoryProfile : Profile
    {
        public TaxCategoryProfile()
        {
            CreateMap<TaxCategory, TaxCategoryResponse>();

            CreateMap<TaxCategoryResponse, TaxCategory>();

            CreateMap<CreateTaxCategoryRequest, CreateTaxCategory>();

            CreateMap<CreateTaxCategory, TaxCategory>();

            CreateMap<UpdateTaxCategoryRequest, UpdateTaxCategory>();

            CreateMap<UpdateTaxCategory, TaxCategory>();

            CreateMap<DeleteTaxCategoryRequest, DeleteTaxCategory>();
        }
    }
}