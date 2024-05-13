using System.Reflection;
using Adanom.Ecommerce.API.Data.Attributes;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetStockUnitTypesHandler : IRequestHandler<GetStockUnitTypes, IEnumerable<StockUnitTypeResponse>>

    {
        #region Fields

        #endregion

        #region Ctor

        public GetStockUnitTypesHandler()
        {
        }

        #endregion

        #region IRequestHandler Members

        public Task<IEnumerable<StockUnitTypeResponse>> Handle(GetStockUnitTypes command, CancellationToken cancellationToken)
        {
            var responses = new List<StockUnitTypeResponse>();
            var types = Enum.GetValues<StockUnitType>();
            var enumType = typeof(StockUnitType);

            foreach (var type in types)
            {
                var typeAsMemberInfo = enumType.GetMember(type.ToString()).Single();

                var response = new StockUnitTypeResponse(type)
                {
                    Name = GetTypeDisplayName(typeAsMemberInfo, type),
                };

                responses.Add(response);
            }

            return Task.FromResult(responses.AsEnumerable());
        }

        #endregion

        #region Private Methods

        private static string GetTypeDisplayName(MemberInfo typeAsMemberInfo, StockUnitType type)
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
