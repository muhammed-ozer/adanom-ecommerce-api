namespace Adanom.Ecommerce.API.Commands.Models
{
    public class TaxCategoryResponse : BaseResponseEntity<long>
    {
        public string Name { get; set; } = null!;

        public string GroupName { get; set; } = null!;

        public byte Rate { get; set; }

        public int DisplayOrder { get; set; }
    }
}