namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ProductSKUProfile : Profile
    {
        public ProductSKUProfile()
        {
            CreateMap<ProductSKU, ProductSKUResponse>()
                .ForMember(member => member.StockUnitType, options =>
                    options.MapFrom(e => new StockUnitTypeResponse(e.StockUnitType)));

            CreateMap<ProductSKUResponse, ProductSKU>()
                .ForMember(member => member.StockUnitType, options => 
                    options.MapFrom(e => e.StockUnitType.Key));

            CreateMap<CreateProductSKURequest, CreateProductSKU>();

            CreateMap<CreateProductSKU, ProductSKU>();

            CreateMap<UpdateProductSKUStockRequest, UpdateProductSKUStock>();

            CreateMap<UpdateProductSKUStock, ProductSKU>();

            CreateMap<UpdateProductSKUBarcodesRequest, UpdateProductSKUBarcodes>();

            CreateMap<UpdateProductSKUBarcodes, ProductSKU>();

            CreateMap<UpdateProductSKUInstallmentRequest, UpdateProductSKUInstallment>();

            CreateMap<UpdateProductSKUInstallment, ProductSKU>();

            CreateMap<BatchUpdateProductSKUStocksRequest, BatchUpdateProductSKUStocks>();

            CreateMap<DeleteProductSKURequest, DeleteProductSKU>();
        }
    }
}