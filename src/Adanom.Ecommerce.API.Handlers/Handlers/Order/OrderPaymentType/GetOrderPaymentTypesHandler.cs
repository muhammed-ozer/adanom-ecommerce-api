using System.Collections.Concurrent;
using System.Reflection;
using Adanom.Ecommerce.API.Data.Attributes;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrderPaymentTypesHandler : IRequestHandler<GetOrderPaymentTypes, IEnumerable<OrderPaymentTypeResponse>>

    {
        #region Fields

        private readonly static ConcurrentDictionary<OrderPaymentType, OrderPaymentTypeResponse> _cache = new();

        #endregion

        #region Ctor

        public GetOrderPaymentTypesHandler()
        {
        }

        #endregion

        #region IRequestHandler Members

        public Task<IEnumerable<OrderPaymentTypeResponse>> Handle(GetOrderPaymentTypes command, CancellationToken cancellationToken)
        {
            if (_cache.Values.Any())
            {
                return Task.FromResult(_cache.Values.AsEnumerable());
            }

            var responses = new List<OrderPaymentTypeResponse>();
            var types = Enum.GetValues<OrderPaymentType>();
            var enumType = typeof(OrderPaymentType);

            foreach (var type in types)
            {
                var typeAsMemberInfo = enumType.GetMember(type.ToString()).Single();

                var response = new OrderPaymentTypeResponse(type)
                {
                    Name = GetTypeDisplayName(typeAsMemberInfo, type),
                    DiscountRate = GetDiscountRate(typeAsMemberInfo, type)
                };

                responses.Add(response);

                _cache.TryAdd(response.Key, response);
            }

            return Task.FromResult(responses.AsEnumerable());
        }

        #endregion

        #region Private Methods

        private static string GetTypeDisplayName(MemberInfo typeAsMemberInfo, OrderPaymentType type)
        {
            var enumDisplayNameAttribute = typeAsMemberInfo.GetCustomAttribute<EnumDisplayNameAttribute>();

            if (enumDisplayNameAttribute == null)
            {
                return type.ToString();
            }

            return enumDisplayNameAttribute.Name;
        }

        private static byte GetDiscountRate(MemberInfo typeAsMemberInfo, OrderPaymentType type)
        {
            var enumDiscountRateAttribute = typeAsMemberInfo.GetCustomAttribute<EnumDiscountRateAttribute>();

            if (enumDiscountRateAttribute == null)
            {
                return 0;
            }

            return enumDiscountRateAttribute.DiscountRate;
        }

        #endregion
    }
}
