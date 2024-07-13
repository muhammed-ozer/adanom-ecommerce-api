namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class FavoriteItemProfile : Profile
    {
        public FavoriteItemProfile()
        {
            CreateMap<FavoriteItem, FavoriteItemResponse>();

            CreateMap<FavoriteItemResponse, FavoriteItem>();

            CreateMap<CreateFavoriteItemRequest, CreateFavoriteItem>();

            CreateMap<CreateFavoriteItem, FavoriteItem>();

            CreateMap<DeleteFavoriteItemRequest, DeleteFavoriteItem>();
        }
    }
}