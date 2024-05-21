namespace Adanom.Ecommerce.API.Commands
{
    public class GetReturnRequestStatusType : IRequest<ReturnRequestStatusTypeResponse>
    {
        #region Ctor

        public GetReturnRequestStatusType(ReturnRequestStatusType stockUnitType)
        {
            ReturnRequestStatusType = stockUnitType;
        }

        #endregion

        #region Properties

        public ReturnRequestStatusType ReturnRequestStatusType { get; set; }

        #endregion
    }
}
