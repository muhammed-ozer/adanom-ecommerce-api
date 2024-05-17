namespace Adanom.Ecommerce.API.Commands
{
    public class GetShippingProvider : IRequest<ShippingProviderResponse?>
    {
        #region Ctor

        public GetShippingProvider(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion
    }
}