namespace Adanom.Ecommerce.API.Commands
{
    public class GetBillingAddress : IRequest<BillingAddressResponse?>
    {
        #region Ctor

        public GetBillingAddress(long id, bool? includeDeleted = null)
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