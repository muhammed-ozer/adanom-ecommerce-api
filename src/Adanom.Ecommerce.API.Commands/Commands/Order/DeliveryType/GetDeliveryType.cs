namespace Adanom.Ecommerce.API.Commands
{
    public class GetDeliveryType : IRequest<DeliveryTypeResponse>
    {
        #region Ctor

        public GetDeliveryType(DeliveryType stockUnitType)
        {
            DeliveryType = stockUnitType;
        }

        #endregion

        #region Properties

        public DeliveryType DeliveryType { get; set; }

        #endregion
    }
}
