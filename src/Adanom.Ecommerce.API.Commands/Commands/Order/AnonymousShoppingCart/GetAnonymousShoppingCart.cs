namespace Adanom.Ecommerce.API.Commands
{
    public class GetAnonymousShoppingCart : IRequest<AnonymousShoppingCartResponse?>
    {
        #region Ctor

        public GetAnonymousShoppingCart(Guid id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public Guid Id { get; set; }

        #endregion
    }
}
