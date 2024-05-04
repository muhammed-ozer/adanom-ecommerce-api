namespace Adanom.Ecommerce.API.Commands
{
    public class AddToCache<TEntity> : INotification where TEntity : BaseResponseEntity<long>
    {
        #region Ctor

        public AddToCache(TEntity entity)
        {
            Entity = entity;
        }

        #endregion

        #region Properties

        public TEntity Entity { get; set; }

        #endregion
    }
}