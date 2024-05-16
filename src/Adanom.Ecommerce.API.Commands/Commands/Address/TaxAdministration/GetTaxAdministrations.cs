using Adanom.Ecommerce.API.Commands.Models;

namespace Adanom.Ecommerce.API.Commands
{
    public class GetTaxAdministrations : IRequest<PaginatedData<TaxAdministrationResponse>>
    {
        #region Ctor

        public GetTaxAdministrations(GetTaxAdministrationsFilter? filter = null, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetTaxAdministrationsFilter? Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
