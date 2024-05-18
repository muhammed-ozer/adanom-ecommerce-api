namespace Adanom.Ecommerce.API.Commands
{
    public class GetPickUpStore : IRequest<PickUpStoreResponse?>
    {
        #region Ctor

        public GetPickUpStore(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion
    }
}