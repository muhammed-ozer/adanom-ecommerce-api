using System.Collections.Concurrent;
using System.Reflection;
using Adanom.Ecommerce.API.Data.Attributes;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetReturnRequestStatusTypesHandler : IRequestHandler<GetReturnRequestStatusTypes, IEnumerable<ReturnRequestStatusTypeResponse>>

    {
        #region Fields

        private readonly static ConcurrentDictionary<ReturnRequestStatusType, ReturnRequestStatusTypeResponse> _cache = new();

        #endregion

        #region Ctor

        public GetReturnRequestStatusTypesHandler()
        {
        }

        #endregion

        #region IRequestHandler Members

        public Task<IEnumerable<ReturnRequestStatusTypeResponse>> Handle(GetReturnRequestStatusTypes command, CancellationToken cancellationToken)
        {
            if (_cache.Values.Any())
            {
                return Task.FromResult(_cache.Values.AsEnumerable());
            }

            var responses = new List<ReturnRequestStatusTypeResponse>();
            var types = Enum.GetValues<ReturnRequestStatusType>();
            var enumType = typeof(ReturnRequestStatusType);

            foreach (var type in types)
            {
                var typeAsMemberInfo = enumType.GetMember(type.ToString()).Single();

                var response = new ReturnRequestStatusTypeResponse(type)
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

        private static string GetTypeDisplayName(MemberInfo typeAsMemberInfo, ReturnRequestStatusType type)
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
