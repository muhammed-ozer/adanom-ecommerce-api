namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductTag : IRequest<ProductTagResponse?>
    {
        #region Ctor

        public GetProductTag(long id)
        {
            Id = id;
        }

        public GetProductTag(string value)
        {
            Value = value;
        }

        #endregion

        #region Properties

        public long Id { get; }

        public string Value { get; } = null!;

        #endregion
    }
}