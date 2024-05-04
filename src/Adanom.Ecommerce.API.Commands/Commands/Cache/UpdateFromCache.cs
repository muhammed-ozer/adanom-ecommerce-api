namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateFromCache<TEntity> : INotification where TEntity : BaseResponseEntity<long>
    {
        #region Ctor

        public UpdateFromCache(TEntity entity)
        {
            Entity = entity;
        }

        #endregion

        #region Properties

        public TEntity Entity { get; set; }

        #endregion
    }
}