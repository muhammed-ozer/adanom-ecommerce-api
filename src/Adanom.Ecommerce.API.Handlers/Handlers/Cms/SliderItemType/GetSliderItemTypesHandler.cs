using System.Collections.Concurrent;
using System.Reflection;
using Adanom.Ecommerce.API.Data.Attributes;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetSliderItemTypesHandler : IRequestHandler<GetSliderItemTypes, IEnumerable<SliderItemTypeResponse>>

    {
        #region Fields

        private readonly static ConcurrentDictionary<SliderItemType, SliderItemTypeResponse> _cache = new();

        #endregion

        #region Ctor

        public GetSliderItemTypesHandler()
        {
        }

        #endregion

        #region IRequestHandler Members

        public Task<IEnumerable<SliderItemTypeResponse>> Handle(GetSliderItemTypes command, CancellationToken cancellationToken)
        {
            if (_cache.Values.Any())
            {
                return Task.FromResult(_cache.Values.AsEnumerable());
            }

            var responses = new List<SliderItemTypeResponse>();
            var types = Enum.GetValues<SliderItemType>();
            var enumType = typeof(SliderItemType);

            foreach (var type in types)
            {
                var typeAsMemberInfo = enumType.GetMember(type.ToString()).Single();

                var response = new SliderItemTypeResponse(type)
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

        private static string GetTypeDisplayName(MemberInfo typeAsMemberInfo, SliderItemType type)
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
