namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationGraphql(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL("/admin/graphql", schemaName: "admin");
                endpoints.MapGraphQL("/auth/graphql", schemaName: "auth");
                endpoints.MapGraphQL("/store/graphql", schemaName: "store");
                endpoints.MapBananaCakePop();
            });

            return applicationBuilder;
        }
    }
}
