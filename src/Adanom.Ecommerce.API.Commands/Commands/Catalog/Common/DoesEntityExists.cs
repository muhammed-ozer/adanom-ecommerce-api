namespace Adanom.Ecommerce.API.Commands
{
    public class DoesEntityExists<TEntity> : IRequest<bool> where TEntity : BaseResponseEntity<long>
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