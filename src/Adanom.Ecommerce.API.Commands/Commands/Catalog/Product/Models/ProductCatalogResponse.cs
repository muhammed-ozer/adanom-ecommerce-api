namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ProductCatalogResponse
    {
        public PaginatedData<ProductResponse> PaginatedDataOfProducts { get; set; } = null!;

        public ProductFilterResponse? ProductFilterResponse { get; set; }
    }
}