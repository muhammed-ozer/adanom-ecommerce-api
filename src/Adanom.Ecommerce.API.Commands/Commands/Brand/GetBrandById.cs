namespace Adanom.Ecommerce.API.Commands
{
    public class GetBrandById : IRequest<BrandResponse?>
    {
        #region Ctor

        public GetBrandById(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; }

        #endregion
    }
}