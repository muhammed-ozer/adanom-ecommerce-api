namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class OrderDocumentQueries
    {
        #region GetDistanceSellingContract

        [AllowAnonymous]
        [GraphQLDescription("Gets distance selling contract")]
        public string GetDistanceSellingContract() => OrderDocumentConstantsConstants.DistanceSellingContract.Document;

        #endregion

        #region GetPreliminaryInformationForm

        [AllowAnonymous]
        [GraphQLDescription("Gets preliminary information form")]
        public string GetPreliminaryInformationForm() => OrderDocumentConstantsConstants.PreliminaryInformationForm.Document;

        #endregion
    }
}
