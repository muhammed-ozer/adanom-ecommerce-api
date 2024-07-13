namespace Adanom.Ecommerce.API.Commands
{
    public class DoesUserEntityNameExists<TEntity> : IRequest<bool> where TEntity : BaseResponseEntity<long>
    {
        #region Ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="excludedEntityId">Assign entity id when exclude entity name</param>
        public DoesUserEntityNameExists(Guid userId, string name, long? excludedEntitydId = null)
        {
            UserId = userId;
            Name = name;
            ExcludedEntityId = excludedEntitydId;
        }

        #endregion

        #region Properties

        public Guid UserId { get; }

        public string Name { get; }

        public long? ExcludedEntityId { get; }

        #endregion
    }
}