namespace Adanom.Ecommerce.API.Commands
{
    public class DoesProductIsActive : IRequest<bool>
    {
        #region Ctor

        public DoesProductIsActive(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; }

        #endregion
    }
}