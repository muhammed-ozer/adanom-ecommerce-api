namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class StockReservationProfile : Profile
    {
        public StockReservationProfile()
        {
            CreateMap<StockReservation, StockReservationResponse>();

            CreateMap<StockReservationResponse, StockReservation>();

            CreateMap<CreateStockReservationRequest, CreateStockReservation>();

            CreateMap<CreateStockReservation, StockReservation>();
        }
    }
}