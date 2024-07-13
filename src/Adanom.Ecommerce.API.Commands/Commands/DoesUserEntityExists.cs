namespace Adanom.Ecommerce.API.Commands
{
    public class DoesUserEntityExists<TEntity> : IRequest<bool> where TEntity : BaseResponseEntity<long>
    {
        #region Ctor

        public DoesUserEntityExists(Guid userId, long id)
        {
            UserId = userId;
            Id = id;
        }

        #endregion

        #region Properties

        public Guid UserId { get; }

        public long Id { get; }

        #endregion
    }
}