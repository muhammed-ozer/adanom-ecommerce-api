namespace Adanom.Ecommerce.API.Commands
{
    public class GetTaxCategory : IRequest<TaxCategoryResponse?>
    {
        #region Ctor

        public GetTaxCategory(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; }

        #endregion
    }
}