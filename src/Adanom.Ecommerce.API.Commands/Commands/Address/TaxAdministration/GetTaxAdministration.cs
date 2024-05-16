namespace Adanom.Ecommerce.API.Commands
{
    public class GetTaxAdministration : IRequest<TaxAdministrationResponse?>
    {
        #region Ctor

        public GetTaxAdministration(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion
    }
}