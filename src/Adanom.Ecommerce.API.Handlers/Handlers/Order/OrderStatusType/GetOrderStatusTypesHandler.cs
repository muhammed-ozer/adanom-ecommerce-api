using System.Collections.Concurrent;
using System.Reflection;
using Adanom.Ecommerce.API.Data.Attributes;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrderStatusTypesHandler : IRequestHandler<GetOrderStatusTypes, IEnumerable<OrderStatusTypeResponse>>

    {
        #region Fields

        private readonly static ConcurrentDictionary<OrderStatusType, OrderStatusTypeResponse> _cache = new();

        #endregion

        #region Ctor

        public GetOrderStatusTypesHandler()
        {
        }

        #endregion

        #region IRequestHandler Members

        public Task<IEnumerable<OrderStatusTypeResponse>> Handle(GetOrderStatusTypes command, CancellationToken cancellationToken)
        {
            if (_cache.Values.Any())
            {
                return Task.FromResult(_cache.Values.AsEnumerable());
            }

            var responses = new List<OrderStatusTypeResponse>();
            var types = Enum.GetValues<OrderStatusType>();
            var enumType = typeof(OrderStatusType);

            foreach (var type in types)
            {
                var typeAsMemberInfo = enumType.GetMember(type.ToString()).Single();

                var response = new OrderStatusTypeResponse(type)
                {
                    Name = GetTypeDisplayName(typeAsMemberInfo, type),
                };

                responses.Add(response);

                _cache.TryAdd(response.Key, response);
            }

            return Task.FromResult(responses.AsEnumerable());
        }

        #endregion

        #region Private Methods

        private static string GetTypeDisplayName(MemberInfo typeAsMemberInfo, OrderStatusType type)
        {
            var enumDisplayNameAttribute = typeAsMemberInfo.GetCustomAttribute<EnumDisplayNameAttribute>();

            if (enumDisplayNameAttribute == null)
            {
                return type.ToString();
            }

            return enumDisplayNameAttribute.Name;
        }

        #endregion
    }
}
