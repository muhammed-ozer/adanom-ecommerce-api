namespace Adanom.Ecommerce.API.Commands
{
    public class RemoveFromCache<TEntity> : INotification where TEntity : BaseResponseEntity<long>
    {
        #region Ctor

        public RemoveFromCache(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion
    }
}