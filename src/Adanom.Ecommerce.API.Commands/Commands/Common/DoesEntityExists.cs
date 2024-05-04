namespace Adanom.Ecommerce.API.Commands
{
    public class DoesEntityExists<TEntity> : IRequest<bool>
    {
        #region Ctor

        public DoesEntityExists(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; }

        #endregion
    }
}