namespace Adanom.Ecommerce.API.Commands
{
    public class GetShippingAddress : IRequest<ShippingAddressResponse?>
    {
        #region Ctor

        public GetShippingAddress(long id, bool? includeDeleted = null)
        {
            Id = id;
            IncludeDeleted = includeDeleted;
        }

        #endregion

        #region Properties

        public long Id { get; }

        public bool? IncludeDeleted { get; set; }

        #endregion
    }
}