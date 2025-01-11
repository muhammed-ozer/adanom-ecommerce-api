namespace Adanom.Ecommerce.API.Commands
{
    public class GetShippingProvider : IRequest<ShippingProviderResponse?>
    {
        #region Ctor

        public GetShippingProvider(long id)
        {
            Id = id;
        }

        public GetShippingProvider(bool isDefault)
        {
            IsDefault = isDefault;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        public bool IsDefault { get; set; }

        #endregion
    }
}