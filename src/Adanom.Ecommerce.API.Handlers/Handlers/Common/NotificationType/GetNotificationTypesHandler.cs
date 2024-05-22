using System.Collections.Concurrent;
using System.Reflection;
using Adanom.Ecommerce.API.Data.Attributes;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetNotificationTypesHandler : IRequestHandler<GetNotificationTypes, IEnumerable<NotificationTypeResponse>>

    {
        #region Fields

        private readonly static ConcurrentDictionary<NotificationType, NotificationTypeResponse> _cache = new();

        #endregion

        #region Ctor

        public GetNotificationTypesHandler()
        {
        }

        #endregion

        #region IRequestHandler Members

        public Task<IEnumerable<NotificationTypeResponse>> Handle(GetNotificationTypes command, CancellationToken cancellationToken)
        {
            if (_cache.Values.Any())
            {
                return Task.FromResult(_cache.Values.AsEnumerable());
            }

            var responses = new List<NotificationTypeResponse>();
            var types = Enum.GetValues<NotificationType>();
            var enumType = typeof(NotificationType);

            foreach (var type in types)
            {
                var typeAsMemberInfo = enumType.GetMember(type.ToString()).Single();

                var response = new NotificationTypeResponse(type)
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

        private static string GetTypeDisplayName(MemberInfo typeAsMemberInfo, NotificationType type)
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
