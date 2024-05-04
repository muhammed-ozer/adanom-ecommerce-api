namespace Adanom.Ecommerce.API.Commands
{
    public class ClearEntityCache<TEntity> : INotification where TEntity : BaseResponseEntity<long>
    {
        #region Ctor

        public ClearEntityCache(long? id = null)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long? Id { get; set; }

        #endregion
    }
}