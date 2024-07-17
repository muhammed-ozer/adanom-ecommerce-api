namespace Adanom.Ecommerce.API.Commands
{
    public class GetShoppingCartTotal : IRequest<decimal>
    {
        #region Ctor

        public GetShoppingCartTotal(long id)
        {
            Id = id;
        }

        public GetShoppingCartTotal(Guid userId)
        {
            UserId = userId;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        public Guid? UserId { get; set; }

        #endregion
    }
}
