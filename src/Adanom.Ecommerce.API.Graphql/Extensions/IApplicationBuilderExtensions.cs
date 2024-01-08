namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationGraphql(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL("/backend/graphql", schemaName: "backend");
                endpoints.MapGraphQL("/frontend/graphql", schemaName: "frontend");
                endpoints.MapBananaCakePop();
            });

            return applicationBuilder;
        }
    }
}
