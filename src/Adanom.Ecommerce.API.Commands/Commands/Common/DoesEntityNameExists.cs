namespace Adanom.Ecommerce.API.Commands
{
    public class DoesEntityNameExists<TEntity> : IRequest<bool> where TEntity : BaseResponseEntity<long>
    {
        #region Ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="excludedEntityId">Assign entity id when exclude entity name</param>
        public DoesEntityNameExists(string name, long? excludedEntitydId = null)
        {
            Name = name;
            ExcludedEntityId = excludedEntitydId;
        }

        #endregion

        #region Properties

        public string Name { get; }

        public long? ExcludedEntityId { get; }

        #endregion
    }
}