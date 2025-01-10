namespace Adanom.Ecommerce.API.Commands
{
    public class GetLocalDeliveryProvider : IRequest<LocalDeliveryProviderResponse?>
    {
        #region Ctor

        public GetLocalDeliveryProvider(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion
    }
}