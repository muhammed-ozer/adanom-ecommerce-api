namespace Adanom.Ecommerce.API.Commands
{
    public class DoesEntityInUse<TEntity> : IRequest<bool> where TEntity : BaseResponseEntity<long>
    {
        #region Ctor

        public DoesEntityInUse(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; }

        #endregion
    }
}