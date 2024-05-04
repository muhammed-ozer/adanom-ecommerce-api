namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandResponse>();

            CreateMap<BrandResponse, Brand>();

            CreateMap<CreateBrandRequest, CreateBrand>();

            CreateMap<CreateBrand, Brand>();

            CreateMap<UpdateBrandRequest, UpdateBrand>();

            CreateMap<UpdateBrand, Brand>();

            CreateMap<UpdateBrandLogoRequest, UpdateBrandLogo>();

            CreateMap<DeleteBrandRequest, DeleteBrand>();
        }
    }
}