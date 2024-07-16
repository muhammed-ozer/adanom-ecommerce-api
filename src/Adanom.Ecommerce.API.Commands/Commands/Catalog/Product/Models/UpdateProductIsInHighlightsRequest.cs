namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateProductIsInHighlightsRequest
    {
        public long Id { get; set; }

        public bool IsInHighlights { get; set; }
    }
}