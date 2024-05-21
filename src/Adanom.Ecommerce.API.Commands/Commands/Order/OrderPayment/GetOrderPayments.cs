namespace Adanom.Ecommerce.API.Commands
{
    public class GetOrderPayments : IRequest<PaginatedData<OrderPaymentResponse>>
    {
        #region Ctor

        public GetOrderPayments(GetOrderPaymentsFilter? filter = null, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetOrderPaymentsFilter? Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
